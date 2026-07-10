namespace AgroApp.Application.Features.Costs.DTOs;

public record MonthlyCostDto(
    int Year,
    int Month,
    decimal FertilizationCost,
    decimal LaborCost,
    decimal IrrigationCost,
    decimal TotalCost
);
