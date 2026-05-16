using MediatR;

namespace AgroApp.Application.Features.CropImages.Commands;

public record AnalyzeImageCommand(
    Guid CropId,
    Guid ImageId
) : IRequest<AnalyzeImageResult>;

public record AnalyzeImageResult(
    string Status,
    string Condition,
    float Confidence,
    string Description,
    List<string> Recommendations
);