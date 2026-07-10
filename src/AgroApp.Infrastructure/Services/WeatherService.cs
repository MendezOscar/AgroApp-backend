using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using AgroApp.Application.Common.Interfaces;

namespace AgroApp.Infrastructure.Services;

public class WeatherService(HttpClient httpClient) : IWeatherService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<WeatherForecastResult?> GetForecastAsync(double lat, double lng)
    {
        var url = string.Format(
            CultureInfo.InvariantCulture,
            "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}" +
            "&daily=temperature_2m_min,precipitation_sum&forecast_days=2&timezone=UTC",
            lat, lng);

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenMeteoResponse>(json);

        var minTemps = result?.Daily?.Temperature2mMin;
        if (minTemps is null || minTemps.Length == 0) return null;

        return new WeatherForecastResult(
            minTemps.Min(),
            result!.Daily!.PrecipitationSum?.Max());
    }

    private record OpenMeteoResponse(
        [property: JsonPropertyName("daily")] DailyBlock? Daily);

    private record DailyBlock(
        [property: JsonPropertyName("temperature_2m_min")] decimal[]? Temperature2mMin,
        [property: JsonPropertyName("precipitation_sum")] decimal[]? PrecipitationSum);
}
