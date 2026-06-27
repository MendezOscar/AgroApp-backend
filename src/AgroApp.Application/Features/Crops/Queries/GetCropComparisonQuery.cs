using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetCropComparisonQuery(Guid FarmId) : IRequest<List<CropComparisonDto>>;
