using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.Enums;
using DR_Tic_Tac_Toe.Models;
using System.Data;
using Dapper;

namespace DR_Tic_Tac_Toe.DB
{
    public class DatabaseInitializer
    {
        private readonly HashingService _hashingService;
        private readonly Database _database;

        public DatabaseInitializer(HashingService hashingService, Database database)
        {
            _hashingService = hashingService;
            _database = database;
        }


        #region Initialize
        public async Task InitializeDB()
        {
            using var connection = _database.CreateConnection();

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
        #endregion

        #region Seed
        public async Task SeedDB()
        {
            using var connection = _database.CreateConnection();

            var userCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users;");
            var gameCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Games;");

            if (userCount == 0) await SeedUsers(connection);
            if (gameCount == 0) await SeedGames(connection);
        }

        private async Task SeedUsers(IDbConnection connection)
        {
            var hashedAdminPassword = _hashingService.Hash("admin");

            var users = new User[]
            {
                new User { Username = "admin", PasswordHash = hashedAdminPassword },
                new User { Username = "admin2", PasswordHash = hashedAdminPassword },
                new User { Username = "admin3", PasswordHash = hashedAdminPassword },
                new User { Username = "admin4", PasswordHash = hashedAdminPassword }
            };
            var command = @"INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash);";

            await connection.ExecuteAsync(command, users);
        }

        private async Task SeedGames(IDbConnection connection)
        {
            var games = new Game[]
            {
                new Game {
                    StartTime = DateTime.Now.AddDays(-10),
                    Player1Id = 1,
                    Player2Id = 2,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 1
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-9),
                    Player1Id = 1,
                    Player2Id = 3,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 1
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-8),
                    Player1Id = 1,
                    Player2Id = 3,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 1
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-10),
                    Player1Id = 1,
                    Player2Id = 4,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 1
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-1),
                    Player1Id = 2,
                    Player2Id = 1,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 2
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-9),
                    Player1Id = 2,
                    Player2Id = 3,
                    Status = (int)GameStatus.InProgress,
                    TurnCount = 5,
                    BoardState = "112200000",
                    WinnerId = null
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-2),
                    Player1Id = 2,
                    Player2Id = 4,
                    Status = (int)GameStatus.InProgress,
                    TurnCount = 5,
                    BoardState = "112200000",
                    WinnerId = null
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-1),
                    Player1Id = 2,
                    Player2Id = 4,
                    Status = (int)GameStatus.Completed,
                    TurnCount = 5,
                    BoardState = "111220000",
                    WinnerId = 2
                },
                new Game {
                    StartTime = DateTime.Now,
                    Player1Id = 2,
                    Player2Id = null,
                    Status = (int)GameStatus.Open,
                    TurnCount = 1,
                    BoardState = "100000000",
                    WinnerId = null
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-1),
                    Player1Id = 3,
                    Player2Id = null,
                    Status = (int)GameStatus.Open,
                    TurnCount = 1,
                    BoardState = "100000000",
                    WinnerId = null
                },
                new Game {
                    StartTime = DateTime.Now.AddDays(-4),
                    Player1Id = 4,
                    Player2Id = null,
                    Status = (int)GameStatus.Open,
                    TurnCount = 1,
                    BoardState = "100000000",
                    WinnerId = null
                },
            };

            var command = @"
            INSERT INTO Games (StartTime, Player1Id, Player2Id, Status, TurnCount, BoardState, WinnerId)
            VALUES (@StartTime, @Player1Id, @Player2Id, @Status, @TurnCount, @BoardState, @WinnerId);";

            await connection.ExecuteAsync(command, games);
        }

        #endregion
    }
}
