using Dapper;
using DR_Tic_Tac_Toe.DTOs.User;
using DR_Tic_Tac_Toe.Models;

namespace DR_Tic_Tac_Toe.DB.Repositories
{
    public class UserRepository
    {
        private readonly Database _database;

        public UserRepository(Database database)
        {
            _database = database;
        }
        public async Task<User?> GetByUsername(string username)
        {
            using var connection = _database.CreateConnection();

            var query = "SELECT * FROM Users WHERE Username = @Username LIMIT 1;";

            return await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
        }

        public async Task<bool> Add(User user)
        {
            using var connection = _database.CreateConnection();

            var command = @"
            INSERT INTO Users (Username, PasswordHash) 
            VALUES (@Username, @PasswordHash);";

            var rowsAffected = await connection.ExecuteAsync(command, new User
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash
            });

            return rowsAffected > 0;
        }

        public async Task<UserDetailsDto?> GetDetails(int userId)
        {
            using var connection = _database.CreateConnection();

            var query = @"
                SELECT 
                    u.Username,
                    SUM(CASE WHEN g.Status = 2 THEN 1 ELSE 0 END) as GamesPlayed,
                    SUM(CASE WHEN g.WinnerId = @UserId THEN 1 ELSE 0 END) AS GamesWon,
                    SUM(CASE WHEN g.WinnerId != @UserId AND g.WinnerId IS NOT NULL THEN 1 ELSE 0 END) AS GamesLost
                FROM Users u
                LEFT JOIN Games g ON u.Id = g.Player1Id OR u.Id = g.Player2Id
                WHERE u.Id = @UserId
                GROUP BY u.Username;";

            return await connection.QueryFirstOrDefaultAsync<UserDetailsDto>(query, new { UserId = userId });
        }
    }
}
