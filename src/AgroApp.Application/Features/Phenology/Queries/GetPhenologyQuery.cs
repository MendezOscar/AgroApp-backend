using AgroApp.Application.Features.Phenology.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Phenology.Queries;

public record GetPhenologyQuery(Guid CropId) : IRequest<List<PhenologyStageDto>>;
public record GetTemplatesQuery(string CropType) : IRequest<List<PhenologyTemplateDto>>;