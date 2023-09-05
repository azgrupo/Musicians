using Dapper;
using Microsoft.Data.Sqlite;

namespace Musicians.Infrastructure;

public class DatabaseBootstrap : IDatabaseBootstrap
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public void Setup()
    {
        using var connection = new SqliteConnection($"Data Source={_databaseConfig.DatabaseName}");
        try
        {
            connection.Open();

            connection.Execute("CREATE TABLE IF NOT EXISTS musicians (" +
                "id integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "name VARCHAR(80) NOT NULL, " +
                "genre VARCHAR(20) NOT NULL, " +
                "perform_as VARCHAR(20) NOT NULL, " +
                "intro_date DATETIME NOT NULL, " +
                "instrument VARCHAR(20) " +
                ");");

            connection.Execute("CREATE TABLE IF NOT EXISTS users (" +
                "id integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "email VARCHAR(80) NOT NULL, " +
                "password VARCHAR(20) NOT NULL " +
                ");");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}
