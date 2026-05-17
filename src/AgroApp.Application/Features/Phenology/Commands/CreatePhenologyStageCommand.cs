using AgroApp.Application.Features.Phenology.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Phenology.Commands;

public record CreatePhenologyStageCommand(
    Guid CropId,
    Guid? TemplateId,
    string StageName,
    int StageOrder,
    DateOnly StartedAt,
    string? Observations,
    bool IsCustom
) : IRequest<PhenologyStageDto>;