using Dapper;
using Microsoft.Data.Sqlite;
using Musicians.Application.Interfaces;
using Musicians.Domain.Entities;

namespace Musicians.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public UserRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task<User> AddAsync(User user)
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        int rowsAffected = await connection.ExecuteAsync("INSERT INTO users (email, password) " +
            "VALUES (@Email, @Password);", user);

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");

        var parameter = new
        {
            Email = email
        };

        var found = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE email = @Email", parameter);
        return found;
    }
}
