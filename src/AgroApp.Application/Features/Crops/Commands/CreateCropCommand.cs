using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Commands;

public record CreateCropCommand(
    Guid PlotId,
    string CropType,
    string? Variety,
    DateOnly PlantedAt,
    DateOnly? EstimatedHarvest,
    string? Notes
) : IRequest<CropDto>;