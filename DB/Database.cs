using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DR_Tic_Tac_Toe.DB
{
    public class Database
    {
        protected readonly IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_configuration.GetConnectionString("Database"));
        }

        public async Task InitializeDB()
        {
            using var connection = CreateConnection();

            await CreateUsersTable(connection);
            await CreateGamesTable(connection);
        }

        private async Task CreateUsersTable(IDbConnection connection)
        {
            var command = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    PasswordHash TEXT NOT NULL
                );
            ";

            await connection.ExecuteAsync(command);
        }

        private async Task CreateGamesTable(IDbConnection connection)
        {
            var command = @"
            CREATE TABLE IF NOT EXISTS Games (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StartTime DATETIME NOT NULL,
                Player1Id INTEGER,
                Player2Id INTEGER,
                Status INTEGER NOT NULL,
                TurnCount INTEGER NOT NULl,
                BoardState TEXT NOT NULl,
                WinnerId INTEGER,
                FOREIGN KEY (Player1Id) REFERENCES Users(Id),
                FOREIGN KEY (Player2Id) REFERENCES Users(Id),
                FOREIGN KEY (WinnerId) REFERENCES Users(Id)
            );";

            await connection.ExecuteAsync(command);
        }
    }
}
