using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Commands;

public record SetIrrigationCostCommand(
    Guid CropId,
    Guid Id,
    decimal Cost
) : IRequest<IrrigationDto?>;
