using System.Data.Common;
using CinemaTicketing.Core.Data;
using CinemaTicketing.Core.Entities;
using CinemaTicketing.Core.Enums;
using CinemaTicketing.Core.Repositories;

namespace CinemaTicketing.Data.Repositories;

public sealed class ScreeningRepository(IMySqlConnectionFactory connectionFactory) : IScreeningRepository
{
    public async Task EnsureScreeningsGeneratedForDateAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        // Fetch configured theatre settings & audis
        const string tsSql = """
SELECT ts.id, ts.show_type_id, ts.show_time, ts.price, a.id AS audi_id
FROM theatre_settings ts
INNER JOIN audis a ON a.cinema_id = ts.cinema_id
WHERE ts.cinema_id = @cinemaId;
""";
        await using var tsCmd = connection.CreateCommand();
        tsCmd.CommandText = tsSql;
        AddParameter(tsCmd, "@cinemaId", cinemaId);

        var settings = new List<(long SettingId, long ShowTypeId, TimeSpan ShowTime, decimal Price, long AudiId)>();
        await using (var reader = await tsCmd.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                settings.Add((reader.GetInt64(0), reader.GetInt64(1), reader.GetFieldValue<TimeSpan>(2), reader.GetDecimal(3), reader.GetInt64(4)));
            }
        }

        foreach (var st in settings)
        {
            // Insert screening if not existing
            const string insertScrSql = """
INSERT INTO screenings (cinema_id, audi_id, show_type_id, show_time, screening_date)
VALUES (@cinemaId, @audiId, @showTypeId, @showTime, @screeningDate)
ON DUPLICATE KEY UPDATE id = LAST_INSERT_ID(id);
SELECT LAST_INSERT_ID();
""";
            await using var scrCmd = connection.CreateCommand();
            scrCmd.CommandText = insertScrSql;
            AddParameter(scrCmd, "@cinemaId", cinemaId);
            AddParameter(scrCmd, "@audiId", st.AudiId);
            AddParameter(scrCmd, "@showTypeId", st.ShowTypeId);
            AddParameter(scrCmd, "@showTime", st.ShowTime);
            AddParameter(scrCmd, "@screeningDate", date.ToDateTime(TimeOnly.MinValue));

            var screeningIdObj = await scrCmd.ExecuteScalarAsync(cancellationToken);
            var screeningId = Convert.ToInt64(screeningIdObj);

            // Populate screening_seats from audi_layout_cells
            const string populateSeatsSql = """
INSERT IGNORE INTO screening_seats (screening_id, audi_layout_cell_id, status)
SELECT @screeningId, alc.id, 'AVAILABLE'
FROM audi_layout_cells alc
WHERE alc.audi_id = @audiId AND alc.is_seat = 1;
""";
            await using var seatsCmd = connection.CreateCommand();
            seatsCmd.CommandText = populateSeatsSql;
            AddParameter(seatsCmd, "@screeningId", screeningId);
            AddParameter(seatsCmd, "@audiId", st.AudiId);
            await seatsCmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    public async Task<IReadOnlyList<Screening>> GetScreeningsAsync(long cinemaId, DateOnly date, CancellationToken cancellationToken = default)
    {
        await EnsureScreeningsGeneratedForDateAsync(cinemaId, date, cancellationToken);

        const string sql = """
SELECT s.id, s.cinema_id, s.audi_id, a.name AS audi_name, s.show_type_id, st.name AS show_type_name,
       s.show_time, s.screening_date, s.movie_id, COALESCE(m.name, ''), COALESCE(m.is_3d, 0), s.is_current_show,
       ts.price
FROM screenings s
INNER JOIN audis a ON a.id = s.audi_id
INNER JOIN show_types st ON st.id = s.show_type_id
LEFT JOIN movies m ON m.id = s.movie_id
LEFT JOIN theatre_settings ts ON ts.cinema_id = s.cinema_id AND ts.show_type_id = s.show_type_id AND ts.show_time = s.show_time
WHERE s.cinema_id = @cinemaId AND s.screening_date = @screeningDate
ORDER BY a.display_order, st.display_order, s.show_time;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@cinemaId", cinemaId);
        AddParameter(command, "@screeningDate", date.ToDateTime(TimeOnly.MinValue));

        var list = new List<Screening>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new Screening
            {
                Id = reader.GetInt64(0),
                CinemaId = reader.GetInt64(1),
                AudiId = reader.GetInt64(2),
                AudiName = reader.GetString(3),
                ShowTypeId = reader.GetInt64(4),
                ShowTypeName = reader.GetString(5),
                ShowTime = reader.GetFieldValue<TimeSpan>(6),
                ScreeningDate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                MovieId = reader.IsDBNull(8) ? null : reader.GetInt64(8),
                MovieName = reader.GetString(9),
                IsMovie3D = reader.GetBoolean(10),
                IsCurrentShow = reader.GetBoolean(11),
                DefaultPrice = reader.IsDBNull(12) ? 0m : reader.GetDecimal(12)
            });
        }

        return list;
    }

    public async Task<Screening?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
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
WHERE s.id = @id;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            return new Screening
            {
                Id = reader.GetInt64(0),
                CinemaId = reader.GetInt64(1),
                AudiId = reader.GetInt64(2),
                AudiName = reader.GetString(3),
                ShowTypeId = reader.GetInt64(4),
                ShowTypeName = reader.GetString(5),
                ShowTime = reader.GetFieldValue<TimeSpan>(6),
                ScreeningDate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                MovieId = reader.IsDBNull(8) ? null : reader.GetInt64(8),
                MovieName = reader.GetString(9),
                IsMovie3D = reader.GetBoolean(10),
                IsCurrentShow = reader.GetBoolean(11),
                DefaultPrice = reader.GetDecimal(12)
            };
        }

        return null;
    }

    public async Task SetMovieAssignmentAsync(long cinemaId, DateOnly date, long audiId, long showTypeId, TimeSpan showTime, long movieId, CancellationToken cancellationToken = default)
    {
        await EnsureScreeningsGeneratedForDateAsync(cinemaId, date, cancellationToken);

        const string sql = """
UPDATE screenings
SET movie_id = @movieId
WHERE cinema_id = @cinemaId AND screening_date = @screeningDate AND audi_id = @audiId AND show_type_id = @showTypeId AND show_time = @showTime;
""";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@movieId", movieId);
        AddParameter(command, "@cinemaId", cinemaId);
        AddParameter(command, "@screeningDate", date.ToDateTime(TimeOnly.MinValue));
        AddParameter(command, "@audiId", audiId);
        AddParameter(command, "@showTypeId", showTypeId);
        AddParameter(command, "@showTime", showTime);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task SetCurrentShowAsync(long screeningId, CancellationToken cancellationToken = default)
    {
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            // Clear current show for same cinema & date
            const string clearSql = """
UPDATE screenings s1
INNER JOIN screenings s2 ON s1.cinema_id = s2.cinema_id AND s1.screening_date = s2.screening_date
SET s1.is_current_show = 0
WHERE s2.id = @screeningId;
""";
            await using (var clearCmd = connection.CreateCommand())
            {
                clearCmd.Transaction = transaction;
                clearCmd.CommandText = clearSql;
                AddParameter(clearCmd, "@screeningId", screeningId);
                await clearCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            const string setSql = "UPDATE screenings SET is_current_show = 1 WHERE id = @screeningId;";
            await using (var setCmd = connection.CreateCommand())
            {
                setCmd.Transaction = transaction;
                setCmd.CommandText = setSql;
                AddParameter(setCmd, "@screeningId", screeningId);
                await setCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<IReadOnlyList<ScreeningSeat>> GetScreeningSeatsAsync(long screeningId, CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT ss.id, ss.screening_id, ss.audi_layout_cell_id, alc.row_index, alc.col_index, alc.is_seat,
       alc.row_label, alc.seat_number, alc.seat_class_id, COALESCE(sc.name, ''), alc.is_damaged,
       ss.status, COALESCE(scp.price, ts.price, 0) AS calculated_price
FROM screening_seats ss
INNER JOIN screenings s ON s.id = ss.screening_id
INNER JOIN audi_layout_cells alc ON alc.id = ss.audi_layout_cell_id
LEFT JOIN seat_classes sc ON sc.id = alc.seat_class_id
LEFT JOIN theatre_settings ts ON ts.cinema_id = s.cinema_id AND ts.show_type_id = s.show_type_id AND ts.show_time = s.show_time
LEFT JOIN show_class_prices scp ON scp.theatre_setting_id = ts.id AND scp.seat_class_id = alc.seat_class_id
WHERE ss.screening_id = @screeningId
ORDER BY alc.row_index, alc.col_index;
""";

        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@screeningId", screeningId);

        var list = new List<ScreeningSeat>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var statusStr = reader.GetString(11);
            Enum.TryParse<SeatStatus>(statusStr, true, out var status);

            list.Add(new ScreeningSeat
            {
                Id = reader.GetInt64(0),
                ScreeningId = reader.GetInt64(1),
                AudiLayoutCellId = reader.GetInt64(2),
                RowIndex = reader.GetInt32(3),
                ColIndex = reader.GetInt32(4),
                IsSeat = reader.GetBoolean(5),
                RowLabel = reader.GetString(6),
                SeatNumber = reader.GetString(7),
                SeatClassId = reader.IsDBNull(8) ? null : reader.GetInt64(8),
                SeatClassName = reader.GetString(9),
                IsDamaged = reader.GetBoolean(10),
                Status = status,
                Price = reader.GetDecimal(12)
            });
        }

        return list;
    }

    public async Task UpdateScreeningSeatStatusAsync(long screeningSeatId, SeatStatus status, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE screening_seats SET status = @status WHERE id = @id;";
        await using var connection = connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameter(command, "@id", screeningSeatId);
        AddParameter(command, "@status", status.ToString().ToUpperInvariant());

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
