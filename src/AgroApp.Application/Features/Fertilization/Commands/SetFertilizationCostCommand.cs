using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Commands;

public record SetFertilizationCostCommand(
    Guid CropId,
    Guid Id,
    decimal Cost
) : IRequest<FertilizationDto?>;
