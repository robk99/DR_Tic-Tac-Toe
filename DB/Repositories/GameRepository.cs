using Dapper;
using DR_Tic_Tac_Toe.DTOs;
using DR_Tic_Tac_Toe.DTOs.Requests;

namespace DR_Tic_Tac_Toe.DB.Repositories
{
    public class GameRepository
    {
        private readonly Database _database;

        public GameRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<GameDto>> GetGamesFiltered(GamesFilteredRequest request)
        {
            using var connection = _database.CreateConnection();

            var query = @"
                WITH GameDetails AS (
                    SELECT 
                        g.Id,
                        g.StartTime,
                        g.Player1Id,
                        g.Player2Id,
                        MAX(CASE WHEN u.Id = g.Player1Id THEN u.Username END) AS Player1,
                        MAX(CASE WHEN u.Id = g.Player2Id THEN u.Username END) AS Player2,
                        MAX(CASE WHEN u.Id = g.WinnerId THEN u.Username END) AS Winner,
                        g.Status
                    FROM 
                        games g
                    LEFT JOIN users u
                        ON u.Id IN (g.Player1Id, g.Player2Id, g.WinnerId)
                    GROUP BY g.Id, g.StartTime, g.Player1Id, g.Player2Id, g.WinnerId, g.Status
                )
                SELECT *
                FROM GameDetails
                WHERE 
                    (@PlayerId IS NULL OR Player1Id = @PlayerId OR Player2Id = @PlayerId)
                    AND (@BeforeDate IS NULL OR StartTime < @BeforeDate)
                    AND (@AfterDate IS NULL OR StartTime > @AfterDate)
                    AND (@Status IS NULL OR Status = @Status)
                ORDER BY StartTime DESC
                LIMIT @Size OFFSET @Offset;
                ";

            var parameters = new DynamicParameters();
            parameters.Add("@PlayerId", request.PlayerId);
            parameters.Add("@BeforeDate", request.BeforeDate);
            parameters.Add("@AfterDate", request.AfterDate);
            parameters.Add("@Status", request.Status);
            parameters.Add("@Size", request.Size);
            parameters.Add("@Offset", (request.Page - 1) * request.Size);

            return await connection.QueryAsync<GameDto>(query, parameters);
        }
    }
}
