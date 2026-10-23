using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class BookingRepository(IMySqlConnectionFactory connectionFactory) : IBookingRepository
{
    public async Task<Booking?> GetByIdAsync(long bookingId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, screening_id, user_id, booking_number, booking_type, payment_mode,
       customer_name, customer_phone, customer_address, ticket_amount,
       reservation_amount, three_d_amount, tax_amount, total_amount, created_utc
FROM bookings
WHERE id = @id;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", bookingId);

        Booking? booking = null;
        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            if (await reader.ReadAsync(cancellationToken))
            {
                booking = MapBooking(reader);
            }
        }

        if (booking is not null)
        {
            booking.Seats.AddRange(await GetBookingSeatsAsync(connection, booking.Id, cancellationToken));
        }

        return booking;
    }

    public async Task<Booking?> GetByBookingNumberAsync(string bookingNumber, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, screening_id, user_id, booking_number, booking_type, payment_mode,
       customer_name, customer_phone, customer_address, ticket_amount,
       reservation_amount, three_d_amount, tax_amount, total_amount, created_utc
FROM bookings
WHERE booking_number = @bookingNumber;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@bookingNumber", bookingNumber);

        Booking? booking = null;
        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            if (await reader.ReadAsync(cancellationToken))
            {
                booking = MapBooking(reader);
            }
        }

        if (booking is not null)
        {
            booking.Seats.AddRange(await GetBookingSeatsAsync(connection, booking.Id, cancellationToken));
        }

        return booking;
    }

    public async Task<Booking?> GetLatestBookingAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, screening_id, user_id, booking_number, booking_type, payment_mode,
       customer_name, customer_phone, customer_address, ticket_amount,
       reservation_amount, three_d_amount, tax_amount, total_amount, created_utc
FROM bookings
ORDER BY id DESC
LIMIT 1;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        Booking? booking = null;
        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            if (await reader.ReadAsync(cancellationToken))
            {
                booking = MapBooking(reader);
            }
        }

        if (booking is not null)
        {
            booking.Seats.AddRange(await GetBookingSeatsAsync(connection, booking.Id, cancellationToken));
        }

        return booking;
    }

    public async Task<IReadOnlyList<Booking>> GetForScreeningSeatsAsync(
        long screeningId,
        IEnumerable<long> screeningSeatIds,
        CancellationToken cancellationToken = default)
    {
        var seatIds = screeningSeatIds.Distinct().ToList();
        if (seatIds.Count == 0)
        {
            return Array.Empty<Booking>();
        }

        var inClause = string.Join(",", seatIds.Select((_, i) => $"@seatId{i}"));
        var sql = $"""
SELECT DISTINCT b.id
FROM bookings b
INNER JOIN booking_seats bs ON bs.booking_id = b.id
WHERE b.screening_id = @screeningId
  AND bs.screening_seat_id IN ({inClause});
""";

        var bookingIds = new List<long>();
        await using (var connection = connectionFactory.CreateConnection())
        {
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = sql;
            AddParameter(command, "@screeningId", screeningId);
            for (int i = 0; i < seatIds.Count; i++)
            {
                AddParameter(command, $"@seatId{i}", seatIds[i]);
            }

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                bookingIds.Add(reader.GetInt64(0));
            }
        }

        var bookings = new List<Booking>(bookingIds.Count);
        foreach (var bookingId in bookingIds)
        {
            var booking = await GetByIdAsync(bookingId, cancellationToken);
            if (booking is not null)
            {
                bookings.Add(booking);
            }
        }

        return bookings;
    }

    public async Task MarkTicketPrintedAsync(
        IEnumerable<long> bookingIds,
        IEnumerable<long> confirmedReservationBookingIds,
        CancellationToken cancellationToken = default)
    {
        var ids = bookingIds.Distinct().ToList();
        var confirmedIds = confirmedReservationBookingIds.Distinct().ToList();
        if (ids.Count == 0)
        {
            throw new ArgumentException("At least one booking is required.", nameof(bookingIds));
        }

        if (confirmedIds.Any(id => !ids.Contains(id)))
        {
            throw new ArgumentException("Confirmed reservations must be included in the printed bookings.", nameof(confirmedReservationBookingIds));
        }

        var bookingInClause = string.Join(",", ids.Select((_, i) => $"@bookingId{i}"));
        var confirmedInClause = string.Join(",", confirmedIds.Select((_, i) => $"@confirmedId{i}"));

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            if (confirmedIds.Count > 0)
            {
                var confirmSql = $"""
UPDATE bookings
SET booking_type = 'COUNTER_TICKET'
WHERE id IN ({confirmedInClause})
  AND booking_type IN ('COUNTER_RESERVED', 'TELEPHONE_RESERVED');
""";
                await using var confirmCommand = connection.CreateCommand();
                confirmCommand.Transaction = transaction;
                confirmCommand.CommandText = confirmSql;
                for (int i = 0; i < confirmedIds.Count; i++)
                {
                    AddParameter(confirmCommand, $"@confirmedId{i}", confirmedIds[i]);
                }

                var confirmedCount = await confirmCommand.ExecuteNonQueryAsync(cancellationToken);
                if (confirmedCount != confirmedIds.Count)
                {
                    throw new InvalidOperationException("One or more reservations could not be confirmed.");
                }
            }

            var markSql = $"""
UPDATE screening_seats ss
INNER JOIN booking_seats bs ON bs.screening_seat_id = ss.id
SET ss.is_ticket_printed = 1,
    ss.status = CASE WHEN bs.booking_id IN ({(confirmedIds.Count > 0 ? confirmedInClause : "NULL")})
                     THEN 'SOLD' ELSE ss.status END
WHERE bs.booking_id IN ({bookingInClause});
""";
            await using (var markCommand = connection.CreateCommand())
            {
                markCommand.Transaction = transaction;
                markCommand.CommandText = markSql;
                for (int i = 0; i < ids.Count; i++)
                {
                    AddParameter(markCommand, $"@bookingId{i}", ids[i]);
                }

                for (int i = 0; i < confirmedIds.Count; i++)
                {
                    AddParameter(markCommand, $"@confirmedId{i}", confirmedIds[i]);
                }

                await markCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<long> ProcessBookingTransactionAsync(Booking booking, IEnumerable<long> screeningSeatIds, CancellationToken cancellationToken = default)
    {
        var seatIdsList = screeningSeatIds.ToList();
        if (seatIdsList.Count == 0)
        {
            throw new InvalidOperationException("No seats selected for booking.");
        }

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            // 1. Lock selected screening seats FOR UPDATE and check availability & physical damage
            var inClause = string.Join(",", seatIdsList.Select((_, i) => $"@sId{i}"));
            var lockSql = $"""
SELECT ss.id, ss.status, alc.is_damaged
FROM screening_seats ss
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
WHERE ss.id IN ({inClause})
FOR UPDATE;
""";
            await using (var lockCmd = connection.CreateCommand())
            {
                lockCmd.Transaction = transaction;
                lockCmd.CommandText = lockSql;
                for (int i = 0; i < seatIdsList.Count; i++)
                {
                    AddParameter(lockCmd, $"@sId{i}", seatIdsList[i]);
                }

                await using var reader = await lockCmd.ExecuteReaderAsync(cancellationToken);
                int count = 0;
                while (await reader.ReadAsync(cancellationToken))
                {
                    count++;
                    var statusStr = reader.GetString(1);
                    var isDamaged = reader.GetBoolean(2);

                    if (isDamaged)
                    {
                        throw new InvalidOperationException("One or more selected seats are damaged and cannot be booked.");
                    }

                    if (!string.Equals(statusStr, "AVAILABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidOperationException($"Seat is no longer available (current status: {statusStr}).");
                    }
                }

                if (count != seatIdsList.Count)
                {
                    throw new InvalidOperationException("One or more selected seats could not be located.");
                }
            }

            // 2. Insert booking record
            const string insertBookingSql = """
INSERT INTO bookings (screening_id, user_id, booking_number, booking_type, payment_mode,
                     customer_name, customer_phone, customer_address, ticket_amount,
                     reservation_amount, three_d_amount, tax_amount, total_amount, created_utc)
VALUES (@screeningId, @userId, @bookingNumber, @bookingType, @paymentMode,
        @customerName, @customerPhone, @customerAddress, @ticketAmount,
        @reservationAmount, @threeDAmount, @taxAmount, @totalAmount, @createdUtc);
SELECT LAST_INSERT_ID();
""";
            long bookingId;
            await using (var bkCmd = connection.CreateCommand())
            {
                bkCmd.Transaction = transaction;
                bkCmd.CommandText = insertBookingSql;
                AddParameter(bkCmd, "@screeningId", booking.ScreeningId);
                AddParameter(bkCmd, "@userId", booking.UserId);
                AddParameter(bkCmd, "@bookingNumber", booking.BookingNumber);
                AddParameter(bkCmd, "@bookingType", ToDatabaseBookingType(booking.BookingType));
                AddParameter(bkCmd, "@paymentMode", booking.PaymentMode.ToString());
                AddParameter(bkCmd, "@customerName", booking.CustomerName);
                AddParameter(bkCmd, "@customerPhone", booking.CustomerPhone);
                AddParameter(bkCmd, "@customerAddress", booking.CustomerAddress);
                AddParameter(bkCmd, "@ticketAmount", booking.TicketAmount);
                AddParameter(bkCmd, "@reservationAmount", booking.ReservationAmount);
                AddParameter(bkCmd, "@threeDAmount", booking.ThreeDAmount);
                AddParameter(bkCmd, "@taxAmount", booking.TaxAmount);
                AddParameter(bkCmd, "@totalAmount", booking.TotalAmount);
                AddParameter(bkCmd, "@createdUtc", booking.CreatedUtc);

                var res = await bkCmd.ExecuteScalarAsync(cancellationToken);
                bookingId = Convert.ToInt64(res);
            }

            // 3. Insert booking_seats records & update screening_seats status
            const string insertSeatSql = """
INSERT INTO booking_seats (booking_id, screening_seat_id, seat_class_name, seat_number, price)
VALUES (@bookingId, @screeningSeatId, @seatClassName, @seatNumber, @price);
""";
            const string updateSeatStatusSql = "UPDATE screening_seats SET status = @status WHERE id = @screeningSeatId;";

            var targetStatus = booking.BookingType switch
            {
                BookingType.CounterTicket => "SOLD",
                BookingType.OnlineBooking => "ONLINE_BOOKING",
                BookingType.FreeTicket => "FREE_TICKET",
                BookingType.CounterReservation => "COUNTER_RESERVED",
                BookingType.TelephoneReservation => "TELEPHONE_RESERVED",
                _ => "SOLD"
            };

            foreach (var seat in booking.Seats)
            {
                await using (var bsCmd = connection.CreateCommand())
                {
                    bsCmd.Transaction = transaction;
                    bsCmd.CommandText = insertSeatSql;
                    AddParameter(bsCmd, "@bookingId", bookingId);
                    AddParameter(bsCmd, "@screeningSeatId", seat.ScreeningSeatId);
                    AddParameter(bsCmd, "@seatClassName", seat.SeatClassName);
                    AddParameter(bsCmd, "@seatNumber", seat.SeatNumber);
                    AddParameter(bsCmd, "@price", seat.Price);
                    await bsCmd.ExecuteNonQueryAsync(cancellationToken);
                }

                await using (var statusCmd = connection.CreateCommand())
                {
                    statusCmd.Transaction = transaction;
                    statusCmd.CommandText = updateSeatStatusSql;
                    AddParameter(statusCmd, "@status", targetStatus);
                    AddParameter(statusCmd, "@screeningSeatId", seat.ScreeningSeatId);
                    await statusCmd.ExecuteNonQueryAsync(cancellationToken);
                }
            }

            // 4. Insert payment record
            const string insertPmtSql = """
INSERT INTO payments (booking_id, payment_mode, amount, created_utc)
VALUES (@bookingId, @paymentMode, @amount, @createdUtc);
""";
            await using (var pmtCmd = connection.CreateCommand())
            {
                pmtCmd.Transaction = transaction;
                pmtCmd.CommandText = insertPmtSql;
                AddParameter(pmtCmd, "@bookingId", bookingId);
                AddParameter(pmtCmd, "@paymentMode", booking.PaymentMode.ToString());
                AddParameter(pmtCmd, "@amount", booking.TotalAmount);
                AddParameter(pmtCmd, "@createdUtc", booking.CreatedUtc);
                await pmtCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            return bookingId;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static async Task<List<BookingSeat>> GetBookingSeatsAsync(DbConnection connection, long bookingId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT id, booking_id, screening_seat_id, seat_class_name, seat_number, price FROM booking_seats WHERE booking_id = @bookingId;";
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@bookingId", bookingId);

        var seats = new List<BookingSeat>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            seats.Add(new BookingSeat
            {
                Id = reader.GetInt64(0),
                BookingId = reader.GetInt64(1),
                ScreeningSeatId = reader.GetInt64(2),
                SeatClassName = reader.GetString(3),
                SeatNumber = reader.GetString(4),
                Price = reader.GetDecimal(5)
            });
        }
        return seats;
    }

    private static Booking MapBooking(DbDataReader reader) => new()
    {
        Id = reader.GetInt64(0),
        ScreeningId = reader.GetInt64(1),
        UserId = reader.GetInt64(2),
        BookingNumber = reader.GetString(3),
        BookingType = ParseBookingType(reader.GetString(4)),
        PaymentMode = Enum.Parse<PaymentMode>(reader.GetString(5), true),
        CustomerName = reader.GetString(6),
        CustomerPhone = reader.GetString(7),
        CustomerAddress = reader.GetString(8),
        TicketAmount = reader.GetDecimal(9),
        ReservationAmount = reader.GetDecimal(10),
        ThreeDAmount = reader.GetDecimal(11),
        TaxAmount = reader.GetDecimal(12),
        TotalAmount = reader.GetDecimal(13),
        CreatedUtc = reader.GetDateTime(14)
    };

    private static string ToDatabaseBookingType(BookingType bookingType) => bookingType switch
    {
        BookingType.CounterTicket => "COUNTER_TICKET",
        BookingType.OnlineBooking => "ONLINE_BOOKING",
        BookingType.FreeTicket => "FREE_TICKET",
        BookingType.CounterReservation => "COUNTER_RESERVED",
        BookingType.TelephoneReservation => "TELEPHONE_RESERVED",
        _ => throw new ArgumentOutOfRangeException(nameof(bookingType), bookingType, null)
    };

    private static BookingType ParseBookingType(string bookingType) => bookingType.ToUpperInvariant() switch
    {
        "COUNTER_TICKET" => BookingType.CounterTicket,
        "ONLINE_BOOKING" => BookingType.OnlineBooking,
        "FREE_TICKET" => BookingType.FreeTicket,
        "COUNTER_RESERVED" => BookingType.CounterReservation,
        "TELEPHONE_RESERVED" => BookingType.TelephoneReservation,
        _ => Enum.Parse<BookingType>(bookingType, true)
    };

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}
