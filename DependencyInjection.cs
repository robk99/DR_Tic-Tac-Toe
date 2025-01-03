using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.DB;

namespace DR_Tic_Tac_Toe
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<Database>();
            services.AddSingleton<DatabaseInitializer>();
            services.AddScoped<UserRepository>();
            services.AddScoped<GameRepository>();

            return services;
        }
    }
}
