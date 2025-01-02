using DR_Tic_Tac_Toe.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<Database>();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Database>();
    await db.InitializeDB();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
