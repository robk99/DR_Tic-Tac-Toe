using DR_Tic_Tac_Toe;
using DR_Tic_Tac_Toe.Authentication;
using DR_Tic_Tac_Toe.DB;
using DR_Tic_Tac_Toe.Mappers;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHealthChecks();
services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
services.AddScoped<ValidateUserFilter>();
services.AddSingleton<HashingService>();
services
    .AddSingleton<TokenService>()
    .AddJWTAuthentication(builder.Configuration)
    .AddAuthorization();
services.AddDatabase();
services.AddScoped<UserMapper>();
services.AddScoped<GameMapper>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tic-Tac-Toe-API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KS_Application_API v1");
    });
}

app.MapHealthChecks("/api/health");

await app.InitializeAndSeedDB();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
