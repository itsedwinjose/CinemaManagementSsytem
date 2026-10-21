using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class CashoutRepository(IMySqlConnectionFactory connectionFactory) : ICashoutRepository
{
    public async Task<IReadOnlyList<Screening>> GetEligibleCashoutScreeningsAsync(long cinemaId, DateOnly date, int delayMinutes, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT s.id, s.cinema_id, s.audi_id, a.name AS audi_name, s.show_type_id, st.name AS show_type_name,
       s.show_time, s.screening_date, s.movie_id, COALESCE(m.name, ''), COALESCE(m.is_3d, 0), s.is_current_show,
       COALESCE(ts.price, 0)
FROM screenings s
INNER JOIN audis a ON a.id = s.audi_id
INNER JOIN show_types st ON st.id = s.show_type_id
LEFT JOIN movies m ON m.id = s.movie_id
LEFT JOIN theatre_settings ts ON ts.cinema_id = s.cinema_id AND ts.show_type_id = s.show_type_id AND ts.show_time = s.show_time
LEFT JOIN cashout_records cr ON cr.cinema_id = s.cinema_id AND cr.show_date = s.screening_date AND cr.show_time = s.show_time AND cr.show_type_name = st.name AND (cr.audi_id = s.audi_id OR cr.audi_id IS NULL)
WHERE s.cinema_id = @cinemaId
  AND s.screening_date = @screeningDate
  AND cr.id IS NULL
ORDER BY st.display_order, s.show_time;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", cinemaId);
        AddParameter(command, "@screeningDate", date.ToDateTime(TimeOnly.MinValue));

        var screenings = new List<Screening>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var showTime = reader.GetFieldValue<TimeSpan>(6);
            var screeningDateTime = date.ToDateTime(TimeOnly.FromTimeSpan(showTime));
            var cutoffTime = screeningDateTime.AddMinutes(delayMinutes);

            // Filter for eligibility: current local time must be >= cutoffTime
            if (DateTime.Now >= cutoffTime)
            {
                screenings.Add(new Screening
                {
                    Id = reader.GetInt64(0),
                    CinemaId = reader.GetInt64(1),
                    AudiId = reader.GetInt64(2),
                    AudiName = reader.GetString(3),
                    ShowTypeId = reader.GetInt64(4),
                    ShowTypeName = reader.GetString(5),
                    ShowTime = showTime,
                    ScreeningDate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                    MovieId = reader.IsDBNull(8) ? null : reader.GetInt64(8),
                    MovieName = reader.GetString(9),
                    IsMovie3D = reader.GetBoolean(10),
                    IsCurrentShow = reader.GetBoolean(11),
                    DefaultPrice = reader.GetDecimal(12)
                });
            }
        }

        return screenings;
    }

    public async Task<CashoutRecord?> CalculateCashoutForScreeningAsync(long screeningId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT s.cinema_id, s.audi_id, a.name AS audi_name, s.screening_date, s.show_time, st.name AS show_type_name, COALESCE(m.name, '') AS movie_name,
       SUM(CASE WHEN b.booking_type IN ('COUNTER_TICKET', 'ONLINE_BOOKING') THEN 1 ELSE 0 END) AS sold_seats,
       SUM(CASE WHEN b.booking_type = 'FREE_TICKET' THEN 1 ELSE 0 END) AS free_seats,
       SUM(CASE WHEN b.booking_type IN ('COUNTER_RESERVED', 'TELEPHONE_RESERVED') THEN 1 ELSE 0 END) AS reserved_seats,
       COALESCE(SUM(b.ticket_amount), 0) AS ticket_amount,
       COALESCE(SUM(b.reservation_amount), 0) AS reservation_amount,
       COALESCE(SUM(b.three_d_amount), 0) AS three_d_amount,
       COALESCE(SUM(b.total_amount), 0) AS total_amount,
       COALESCE(SUM(CASE WHEN p.payment_mode = 'Cash' THEN p.amount ELSE 0 END), 0) AS cash_amount,
       COALESCE(SUM(CASE WHEN p.payment_mode = 'Card' THEN p.amount ELSE 0 END), 0) AS card_amount,
       COALESCE(SUM(CASE WHEN p.payment_mode = 'Online' THEN p.amount ELSE 0 END), 0) AS online_amount,
       COALESCE(SUM(CASE WHEN p.payment_mode = 'UpiQr' THEN p.amount ELSE 0 END), 0) AS upi_amount
FROM screenings s
INNER JOIN audis a ON a.id = s.audi_id
INNER JOIN show_types st ON st.id = s.show_type_id
LEFT JOIN movies m ON m.id = s.movie_id
LEFT JOIN bookings b ON b.screening_id = s.id
LEFT JOIN payments p ON p.booking_id = b.id
WHERE s.id = @screeningId
GROUP BY s.id, s.cinema_id, s.audi_id, a.name, s.screening_date, s.show_time, st.name, m.name;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@screeningId", screeningId);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            return new CashoutRecord
            {
                CinemaId = reader.GetInt64(0),
                AudiId = reader.GetInt64(1),
                AudiName = reader.GetString(2),
                ShowDate = DateOnly.FromDateTime(reader.GetDateTime(3)),
                ShowTime = reader.GetFieldValue<TimeSpan>(4),
                ShowTypeName = reader.GetString(5),
                MovieName = reader.GetString(6),
                SoldSeats = Convert.ToInt32(reader.GetValue(7)),
                FreeSeats = Convert.ToInt32(reader.GetValue(8)),
                ReservedSeats = Convert.ToInt32(reader.GetValue(9)),
                TicketAmount = reader.GetDecimal(10),
                ReservationAmount = reader.GetDecimal(11),
                ThreeDAmount = reader.GetDecimal(12),
                TotalAmount = reader.GetDecimal(13),
                CashAmount = reader.GetDecimal(14),
                CardAmount = reader.GetDecimal(15),
                OnlineAmount = reader.GetDecimal(16),
                UpiAmount = reader.GetDecimal(17),
                CashedOutAt = DateTime.UtcNow
            };
        }

        return null;
    }

    public async Task SaveCashoutRecordAsync(CashoutRecord record, CancellationToken cancellationToken = default)
    {
        const string sql = """
INSERT INTO cashout_records (cinema_id, audi_id, audi_name, show_date, show_time, show_type_name, movie_name,
                            sold_seats, free_seats, reserved_seats, ticket_amount,
                            reservation_amount, three_d_amount, total_amount,
                            cash_amount, card_amount, online_amount, upi_amount,
                            cashed_out_at, cashed_out_by, is_printed, printed_at, printed_by)
VALUES (@cinemaId, @audiId, @audiName, @showDate, @showTime, @showTypeName, @movieName,
        @soldSeats, @freeSeats, @reservedSeats, @ticketAmount,
        @reservationAmount, @threeDAmount, @totalAmount,
        @cashAmount, @cardAmount, @onlineAmount, @upiAmount,
        @cashedOutAt, @cashedOutBy, @isPrinted, @printedAt, @printedBy);
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", record.CinemaId);
        AddParameter(command, "@audiId", record.AudiId ?? (object)DBNull.Value);
        AddParameter(command, "@audiName", record.AudiName);
        AddParameter(command, "@showDate", record.ShowDate.ToDateTime(TimeOnly.MinValue));
        AddParameter(command, "@showTime", record.ShowTime);
        AddParameter(command, "@showTypeName", record.ShowTypeName);
        AddParameter(command, "@movieName", record.MovieName);
        AddParameter(command, "@soldSeats", record.SoldSeats);
        AddParameter(command, "@freeSeats", record.FreeSeats);
        AddParameter(command, "@reservedSeats", record.ReservedSeats);
        AddParameter(command, "@ticketAmount", record.TicketAmount);
        AddParameter(command, "@reservationAmount", record.ReservationAmount);
        AddParameter(command, "@threeDAmount", record.ThreeDAmount);
        AddParameter(command, "@totalAmount", record.TotalAmount);
        AddParameter(command, "@cashAmount", record.CashAmount);
        AddParameter(command, "@cardAmount", record.CardAmount);
        AddParameter(command, "@onlineAmount", record.OnlineAmount);
        AddParameter(command, "@upiAmount", record.UpiAmount);
        AddParameter(command, "@cashedOutAt", record.CashedOutAt);
        AddParameter(command, "@cashedOutBy", record.CashedOutBy);
        AddParameter(command, "@isPrinted", record.IsPrinted ? 1 : 0);
        AddParameter(command, "@printedAt", record.PrintedAt.HasValue ? record.PrintedAt.Value : DBNull.Value);
        AddParameter(command, "@printedBy", record.PrintedBy ?? (object)DBNull.Value);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CashoutRecord>> GetUnprintedCashoutReportsAsync(long cinemaId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT id, cinema_id, audi_id, audi_name, show_date, show_time, show_type_name, movie_name,
       sold_seats, free_seats, reserved_seats, ticket_amount, reservation_amount,
       three_d_amount, total_amount, cash_amount, card_amount, online_amount,
       upi_amount, cashed_out_at, cashed_out_by, is_printed, printed_at, printed_by
FROM cashout_records
WHERE cinema_id = @cinemaId AND is_printed = 0
ORDER BY show_date, show_time;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", cinemaId);

        var list = new List<CashoutRecord>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(MapCashoutRecord(reader));
        }

        return list;
    }

    public async Task MarkCashoutReportPrintedAsync(long cashoutId, string printedBy, CancellationToken cancellationToken = default)
    {
        const string sql = """
UPDATE cashout_records
SET is_printed = 1, printed_at = UTC_TIMESTAMP(), printed_by = @printedBy
WHERE id = @id;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", cashoutId);
        AddParameter(command, "@printedBy", printedBy);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static CashoutRecord MapCashoutRecord(DbDataReader reader) => new()
    {
        Id = reader.GetInt64(0),
        CinemaId = reader.GetInt64(1),
        AudiId = reader.IsDBNull(2) ? null : reader.GetInt64(2),
        AudiName = reader.GetString(3),
        ShowDate = DateOnly.FromDateTime(reader.GetDateTime(4)),
        ShowTime = reader.GetFieldValue<TimeSpan>(5),
        ShowTypeName = reader.GetString(6),
        MovieName = reader.GetString(7),
        SoldSeats = reader.GetInt32(8),
        FreeSeats = reader.GetInt32(9),
        ReservedSeats = reader.GetInt32(10),
        TicketAmount = reader.GetDecimal(11),
        ReservationAmount = reader.GetDecimal(12),
        ThreeDAmount = reader.GetDecimal(13),
        TotalAmount = reader.GetDecimal(14),
        CashAmount = reader.GetDecimal(15),
        CardAmount = reader.GetDecimal(16),
        OnlineAmount = reader.GetDecimal(17),
        UpiAmount = reader.GetDecimal(18),
        CashedOutAt = reader.GetDateTime(19),
        CashedOutBy = reader.GetString(20),
        IsPrinted = reader.GetBoolean(21),
        PrintedAt = reader.IsDBNull(22) ? null : reader.GetDateTime(22),
        PrintedBy = reader.IsDBNull(23) ? null : reader.GetString(23)
    };

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}
