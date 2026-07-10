namespace AgroApp.Application.Features.CropImages.DTOs;

public record PestDiagnosisSummaryDto(
    string Condition,
    int Count,
    DateTime LastDetectedAt
);
