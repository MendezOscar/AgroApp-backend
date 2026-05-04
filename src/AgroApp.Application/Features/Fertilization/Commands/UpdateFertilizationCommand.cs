using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Commands;

public record UpdateFertilizationCommand(
    Guid CropId,
    Guid Id,
    string ProductName,
    string? ProductType,
    decimal? DoseKgHa,
    decimal? TotalKg,
    string? Method,
    decimal? Cost,
    DateTime AppliedAt,
    DateOnly? NextApplication,
    string? Notes
) : IRequest<FertilizationDto?>;