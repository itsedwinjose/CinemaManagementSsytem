using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class AudiLayoutRepository(IMySqlConnectionFactory connectionFactory) : IAudiLayoutRepository
{
    public async Task<IReadOnlyList<AudiLayoutCell>> GetCellsForAudiAsync(long audiId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT alc.id, alc.audi_id, alc.row_index, alc.col_index, alc.is_seat, alc.row_label, alc.seat_number, alc.seat_class_id, COALESCE(sc.name, ''), alc.is_damaged
FROM audi_layout_cells alc
LEFT JOIN seat_classes sc ON sc.id = alc.seat_class_id
WHERE alc.audi_id = @audiId
ORDER BY alc.row_index, alc.col_index;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@audiId", audiId);

        var list = new List<AudiLayoutCell>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new AudiLayoutCell
            {
                Id = reader.GetInt64(0),
                AudiId = reader.GetInt64(1),
                RowIndex = reader.GetInt32(2),
                ColIndex = reader.GetInt32(3),
                IsSeat = reader.GetBoolean(4),
                RowLabel = reader.GetString(5),
                SeatNumber = reader.GetString(6),
                SeatClassId = reader.IsDBNull(7) ? null : reader.GetInt64(7),
                SeatClassName = reader.GetString(8),
                IsDamaged = reader.GetBoolean(9)
            });
        }

        return list;
    }

    public async Task SaveLayoutCellsAsync(long audiId, int totalRows, int totalCols, IEnumerable<AudiLayoutCell> cells, CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            // 1. Update Audi dimensions
            await using (var updateAudiCmd = connection.CreateCommand())
            {
                updateAudiCmd.Transaction = transaction;
                updateAudiCmd.CommandText = "UPDATE audis SET total_rows = @rows, total_cols = @cols WHERE id = @audiId;";
                AddParameter(updateAudiCmd, "@rows", totalRows);
                AddParameter(updateAudiCmd, "@cols", totalCols);
                AddParameter(updateAudiCmd, "@audiId", audiId);
                await updateAudiCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            // 2. Upsert Layout Cells
            const string upsertSql = """
INSERT INTO audi_layout_cells (audi_id, row_index, col_index, is_seat, row_label, seat_number, seat_class_id, is_damaged)
VALUES (@audiId, @rowIndex, @colIndex, @isSeat, @rowLabel, @seatNumber, @seatClassId, @isDamaged)
ON DUPLICATE KEY UPDATE
    is_seat = VALUES(is_seat),
    row_label = VALUES(row_label),
    seat_number = VALUES(seat_number),
    seat_class_id = VALUES(seat_class_id),
    is_damaged = VALUES(is_damaged);
""";

            foreach (var cell in cells)
            {
                await using var cellCmd = connection.CreateCommand();
                cellCmd.Transaction = transaction;
                cellCmd.CommandText = upsertSql;
                AddParameter(cellCmd, "@audiId", audiId);
                AddParameter(cellCmd, "@rowIndex", cell.RowIndex);
                AddParameter(cellCmd, "@colIndex", cell.ColIndex);
                AddParameter(cellCmd, "@isSeat", cell.IsSeat ? 1 : 0);
                AddParameter(cellCmd, "@rowLabel", cell.RowLabel);
                AddParameter(cellCmd, "@seatNumber", cell.SeatNumber);
                AddParameter(cellCmd, "@seatClassId", cell.SeatClassId.HasValue ? cell.SeatClassId.Value : DBNull.Value);
                AddParameter(cellCmd, "@isDamaged", cell.IsDamaged ? 1 : 0);
                await cellCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteLayoutAsync(long audiId, CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            const string bookedSeatsSql = """
SELECT COUNT(*)
FROM screening_seats ss
INNER JOIN screenings s ON s.id = ss.screening_id
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
INNER JOIN booking_seats bs ON bs.screening_seat_id = ss.id
WHERE alc.audi_id = @audiId;
""";
            await using (var bookedSeatsCommand = connection.CreateCommand())
            {
                bookedSeatsCommand.Transaction = transaction;
                bookedSeatsCommand.CommandText = bookedSeatsSql;
                AddParameter(bookedSeatsCommand, "@audiId", audiId);
                var bookedSeatCount = Convert.ToInt64(
                    await bookedSeatsCommand.ExecuteScalarAsync(cancellationToken));
                if (bookedSeatCount > 0)
                {
                    throw new InvalidOperationException(
                        "This Auditorium has seats linked to bookings. Its layout cannot be deleted without deleting booking history.");
                }
            }

            const string deleteScreeningSeatsSql = """
DELETE ss
FROM screening_seats ss
INNER JOIN screenings s ON s.id = ss.screening_id
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
WHERE alc.audi_id = @audiId;
""";
            await using (var deleteScreeningSeatsCommand = connection.CreateCommand())
            {
                deleteScreeningSeatsCommand.Transaction = transaction;
                deleteScreeningSeatsCommand.CommandText = deleteScreeningSeatsSql;
                AddParameter(deleteScreeningSeatsCommand, "@audiId", audiId);
                await deleteScreeningSeatsCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var deleteLayoutCommand = connection.CreateCommand())
            {
                deleteLayoutCommand.Transaction = transaction;
                deleteLayoutCommand.CommandText = "DELETE FROM audi_layout_cells WHERE audi_id = @audiId;";
                AddParameter(deleteLayoutCommand, "@audiId", audiId);
                await deleteLayoutCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task ReplaceLayoutAsync(
        long audiId,
        int totalRows,
        int totalCols,
        IEnumerable<AudiLayoutCell> cells,
        CancellationToken cancellationToken = default)
    {
        var newCells = cells.ToList();
        if (newCells.Any(cell =>
            cell.RowIndex < 0 || cell.RowIndex >= totalRows
            || cell.ColIndex < 0 || cell.ColIndex >= totalCols))
        {
            throw new ArgumentException("Layout cells must fit within the specified grid dimensions.", nameof(cells));
        }

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            const string bookedSeatsSql = """
SELECT COUNT(*)
FROM screening_seats ss
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
INNER JOIN booking_seats bs ON bs.screening_seat_id = ss.id
WHERE alc.audi_id = @audiId;
""";
            await using (var bookedSeatsCommand = connection.CreateCommand())
            {
                bookedSeatsCommand.Transaction = transaction;
                bookedSeatsCommand.CommandText = bookedSeatsSql;
                AddParameter(bookedSeatsCommand, "@audiId", audiId);
                var bookedSeatCount = Convert.ToInt64(
                    await bookedSeatsCommand.ExecuteScalarAsync(cancellationToken));
                if (bookedSeatCount > 0)
                {
                    throw new InvalidOperationException(
                        "This Auditorium has seats linked to bookings. Its layout cannot be resized without deleting booking history.");
                }
            }

            await using (var deleteScreeningSeatsCommand = connection.CreateCommand())
            {
                deleteScreeningSeatsCommand.Transaction = transaction;
                deleteScreeningSeatsCommand.CommandText = """
DELETE ss
FROM screening_seats ss
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
WHERE alc.audi_id = @audiId;
""";
                AddParameter(deleteScreeningSeatsCommand, "@audiId", audiId);
                await deleteScreeningSeatsCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var deleteLayoutCommand = connection.CreateCommand())
            {
                deleteLayoutCommand.Transaction = transaction;
                deleteLayoutCommand.CommandText = "DELETE FROM audi_layout_cells WHERE audi_id = @audiId;";
                AddParameter(deleteLayoutCommand, "@audiId", audiId);
                await deleteLayoutCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var updateAudiCommand = connection.CreateCommand())
            {
                updateAudiCommand.Transaction = transaction;
                updateAudiCommand.CommandText = "UPDATE audis SET total_rows = @rows, total_cols = @cols WHERE id = @audiId;";
                AddParameter(updateAudiCommand, "@rows", totalRows);
                AddParameter(updateAudiCommand, "@cols", totalCols);
                AddParameter(updateAudiCommand, "@audiId", audiId);
                await updateAudiCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            foreach (var cell in newCells)
            {
                await using var insertCellCommand = connection.CreateCommand();
                insertCellCommand.Transaction = transaction;
                insertCellCommand.CommandText = """
INSERT INTO audi_layout_cells
    (audi_id, row_index, col_index, is_seat, row_label, seat_number, seat_class_id, is_damaged)
VALUES
    (@audiId, @rowIndex, @colIndex, @isSeat, @rowLabel, @seatNumber, @seatClassId, @isDamaged);
""";
                AddParameter(insertCellCommand, "@audiId", audiId);
                AddParameter(insertCellCommand, "@rowIndex", cell.RowIndex);
                AddParameter(insertCellCommand, "@colIndex", cell.ColIndex);
                AddParameter(insertCellCommand, "@isSeat", cell.IsSeat ? 1 : 0);
                AddParameter(insertCellCommand, "@rowLabel", cell.RowLabel);
                AddParameter(insertCellCommand, "@seatNumber", cell.SeatNumber);
                AddParameter(insertCellCommand, "@seatClassId", cell.SeatClassId.HasValue ? cell.SeatClassId.Value : DBNull.Value);
                AddParameter(insertCellCommand, "@isDamaged", cell.IsDamaged ? 1 : 0);
                await insertCellCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateCellDamageStatusAsync(long cellId, bool isDamaged, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE audi_layout_cells SET is_damaged = @isDamaged WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", cellId);
        AddParameter(command, "@isDamaged", isDamaged ? 1 : 0);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static void AddParameter(DbCommand command, string name, object value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        command.Parameters.Add(param);
    }
}
