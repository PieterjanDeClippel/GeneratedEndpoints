using MintPlayer.AspNetCore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// Map the endpoints
app.MapWebApplication10Endpoints();

app.Run();


internal class WeatherForecastGroup : IEndpointGroup
{
    public static string Prefix => "/weatherforecast";
}

[MemberOf<WeatherForecastGroup>]
internal sealed partial class GetWeatherForecasts : IGetEndpoint
{
    public static string Path => "/";

    static string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public async Task<IResult> HandleAsync(HttpContext httpContext)
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return Results.Ok(forecast);
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
