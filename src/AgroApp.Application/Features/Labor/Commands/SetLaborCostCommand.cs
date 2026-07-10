using AgroApp.Application.Features.Labor.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Labor.Commands;

public record SetLaborCostCommand(
    Guid CropId,
    Guid Id,
    decimal Cost
) : IRequest<LaborDto?>;
