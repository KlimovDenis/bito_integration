using Carter;

namespace Api.Endpoints;

public sealed class Weather : ICarterModule
{
    private static readonly IReadOnlyList<string> Summaries = new[]
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
                        Summaries[Random.Shared.Next(Summaries.Count)]
                    ));
                
                return forecast;
            })
            .WithName("GetWeatherForecast");
    }

    private record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}