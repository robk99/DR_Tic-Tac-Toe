
using Microsoft.Data.Sqlite;
using System.Data;

namespace DR_Tic_Tac_Toe.DB
{
    public class Database
    {
        private readonly IConfiguration _configuration;


        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_configuration.GetConnectionString("Database"));
        }

    }
}
