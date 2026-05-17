using AgroApp.Application.Features.Phenology.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Phenology.Commands;

public record UpdatePhenologyStageCommand(
    Guid StageId,
    Guid CropId,
    DateOnly? EndedAt,
    string? Observations
) : IRequest<PhenologyStageDto?>;