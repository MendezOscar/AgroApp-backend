using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Commands;

public record CreateFertilizationCommand(
    Guid CropId,
    string ProductName,
    string? ProductType,
    decimal? DoseKgHa,
    decimal? TotalKg,
    string? Method,
    decimal? Cost,
    DateTime AppliedAt,
    DateOnly? NextApplication,
    string? Notes,
    Guid? TaskId = null,
    Guid? OccurrenceId = null
) : IRequest<FertilizationDto>;