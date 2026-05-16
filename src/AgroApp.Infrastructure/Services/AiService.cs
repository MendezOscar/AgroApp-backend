using System.Text.Json;
using AgroApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AgroApp.Infrastructure.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _aiServiceUrl;

    public AiService(HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _aiServiceUrl = configuration["AiService:Url"]
            ?? "http://localhost:8000";
    }

    public async Task<AiDiagnosisResult> AnalyzeImageAsync(
        string imageUrl,
        string cropType,
        string cropId,
        string imageId)
    {
        var payload = new
        {
            image_url = imageUrl,
            crop_type = cropType,
            crop_id = cropId,
            image_id = imageId,
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(
            json,
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            $"{_aiServiceUrl}/analyze", content);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content
            .ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AiResponse>(
            responseJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return new AiDiagnosisResult(
            result!.Status,
            result.Condition,
            result.Confidence,
            result.Description,
            result.Recommendations);
    }

    private record AiResponse(
        string ImageId,
        string Status,
        string Condition,
        float Confidence,
        string Description,
        List<string> Recommendations,
        List<object> RawLabels);
}