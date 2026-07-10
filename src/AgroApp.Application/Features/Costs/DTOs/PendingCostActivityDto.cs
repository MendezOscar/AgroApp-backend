namespace AgroApp.Application.Features.Costs.DTOs;

public record PendingCostActivityDto(
    Guid Id,
    string ActivityType,
    Guid CropId,
    string CropType,
    string? PlotName,
    DateTime Date,
    string Description
);
