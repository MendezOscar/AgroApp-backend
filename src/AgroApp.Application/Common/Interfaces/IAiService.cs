namespace AgroApp.Application.Common.Interfaces;

public interface IAiService
{
    Task<AiDiagnosisResult> AnalyzeImageAsync(
        string imageUrl,
        string cropType,
        string cropId,
        string imageId);
}

public record AiDiagnosisResult(
    string Status,
    string Condition,
    float Confidence,
    string Description,
    List<string> Recommendations
);