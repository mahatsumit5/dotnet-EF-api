using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

// helps to send and receive json data
[ApiController]
// route connecter=> logic to find WeatherForecaseController
[Route("[controller]")]
// inherit ControllerBase class
public class WeatherForecastController : ControllerBase
{
    private readonly string[] summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching",
    ];

    [HttpGet("", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> GetFiveDaysForeCast()
    {
        var forecast = Enumerable
            .Range(1, 5)
            .Select(index => new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
