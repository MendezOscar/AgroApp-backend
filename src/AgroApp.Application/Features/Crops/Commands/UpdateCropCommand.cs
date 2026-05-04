using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Domain.Enums;
using MediatR;

namespace AgroApp.Application.Features.Crops.Commands;

public record UpdateCropCommand(
    Guid PlotId,
    Guid Id,
    string CropType,
    string? Variety,
    DateOnly PlantedAt,
    DateOnly? EstimatedHarvest,
    DateOnly? HarvestedAt,
    CropStatus Status,
    decimal? YieldKg,
    string? Notes
) : IRequest<CropDto?>;