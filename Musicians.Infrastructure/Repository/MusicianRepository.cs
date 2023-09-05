using Dapper;
using Microsoft.Data.Sqlite;
using Musicians.Application.Interfaces;
using Musicians.Domain.Entities;

namespace Musicians.Infrastructure.Repository;

public class MusicianRepository : IMusicianRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public MusicianRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public async Task<Musician> CreateAsync(Musician musician)
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        var rowsAffected = await connection.ExecuteAsync("INSERT INTO musicians (name, genre, perform_as, intro_date, instrument) " +
            "VALUES (@Name, @Genre, @PerformAs, @IntroDate, @Instrument);", musician);
        if (rowsAffected > 0)
        {
            int id = await connection.ExecuteScalarAsync<int>("SELECT last_insert_rowid()");
            musician.Id = id;
            return musician;
        }

        return musician;
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        var musician = await GetByIdAsync(id);
        if(musician != null)
        {
            await connection.ExecuteAsync("DELETE FROM musicians WHERE id = @Id", musician);
        }
    }

    public async Task<IEnumerable<Musician>> GetAllAsync()
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        return await connection.QueryAsync<Musician>(@"SELECT 
            id,name,genre,perform_as as performAs, intro_date as introDate, instrument FROM musicians");
    }

    public async Task<Musician> GetByIdAsync(int id)
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        var musician = await connection.QueryFirstOrDefaultAsync<Musician>(@"SELECT 
            id,name,genre,perform_as as performAs, intro_date as introDate, instrument 
            FROM musicians WHERE id = @Id", new { Id = id });
        return musician;
    }

    public async Task UpdateAsync(Musician musician)
    {
        var musicianFound = GetByIdAsync(musician.Id);
        if(musicianFound != null)
        {
            using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

            await connection.ExecuteAsync("UPDATE musicians SET " +
                "name = @Name, genre = @Genre, perform_as = @PerformAs, intro_date = @IntroDate, instrument = @Instrument " +
                "WHERE id = @Id", musician);

        }
    }
}
