using Api.Models.Weather;
using Carter;
using System.Threading.Tasks;

namespace Api.Endpoints;

public sealed class Weather : ICarterModule
{
    private IReadOnlyList<string> summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("weather").WithOpenApi();
        
        group.MapGet("/forecasts", () =>
            {
                var forecast = Enumerable.Range(1, 5)
                    .Select(index => new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Count)]
                    ));

                return (forecast, forecast.Count());
            })
            .WithName("GetWeatherForecast");

        group.MapDelete("/forecast", Results.NoContent)
            .WithName("DeleteWeatherForecast");
    }
}