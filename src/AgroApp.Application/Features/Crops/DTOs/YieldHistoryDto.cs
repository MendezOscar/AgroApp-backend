namespace AgroApp.Application.Features.Crops.DTOs;

public record YieldHistoryDto(
    int Year,
    int Month,
    decimal TotalYieldKg,
    int HarvestedCropCount
);
