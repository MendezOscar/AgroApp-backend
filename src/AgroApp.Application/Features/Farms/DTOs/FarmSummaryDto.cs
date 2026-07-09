namespace AgroApp.Application.Features.Farms.DTOs;

public record FarmSummaryDto(
    Guid FarmId,
    string FarmName,
    decimal TotalAreaHa,
    int TotalPlots,
    int ActivePlots,
    int ActiveCropCount,
    decimal TotalCostCurrentMonth,
    int UnresolvedAlertCount
);
