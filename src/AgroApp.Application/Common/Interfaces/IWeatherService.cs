namespace AgroApp.Application.Common.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecastResult?> GetForecastAsync(double lat, double lng);
}

public record WeatherForecastResult(
    decimal MinTemperatureC,
    decimal? MaxPrecipitationMm
);
