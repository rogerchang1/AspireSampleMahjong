var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

//postgredsdb
builder.AddNpgsqlDataSource("postgresdb");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors(static builder =>
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/handscore", (HandModel hand) =>
{
    Console.WriteLine(hand);
    return "test";
})
.WithName("PostRogerTest")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record HandModel(string Hand, bool IsRiichi, Block[] Blocks, bool IsDoubleRiichi, bool IsIppatsu, bool IsHoutei, bool IsHaitei, bool IsRinshan, bool IsChankan, Wind SeatWind, Wind RoundWind, Agari Agari, int DoraCount, string WinTile ) { }

public record Block(string Tile, BlockType Type) { }

public enum BlockType
{
    UNKNOWN,
    PON,
    CHI,
    OPENKAN,
    CLOSEDKAN,
}

public enum Wind
{
    EAST,
    SOUTH,
    WEST,
    NORTH,
}

public enum Agari
{
    TSUMO,
    RON,
}
