using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.DB;
using DR_Tic_Tac_Toe.DB.Repositories;
using DR_Tic_Tac_Toe.Mappers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHealthChecks();
services.AddControllers();
services.AddSingleton<HashingService>();
services.AddSingleton<Database>();
services.AddSingleton<DatabaseInitializer>();
services
    .AddSingleton<TokenService>()
    .AddJWTAuthentication(builder.Configuration)
    .AddAuthorization();

services.AddScoped<UserRepository>();
services.AddScoped<UserMapper>();


var app = builder.Build();

app.MapHealthChecks("/api/health");

{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await dbInitializer.InitializeDB();
    await dbInitializer.SeedDB();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
