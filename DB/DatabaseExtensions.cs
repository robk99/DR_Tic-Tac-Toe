namespace DR_Tic_Tac_Toe.DB
{
    public static class DatabaseExtensions
    {
        public async static Task InitializeAndSeedDB(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
            await dbInitializer.InitializeDB();
            await dbInitializer.SeedDB();
        }
    }
}
