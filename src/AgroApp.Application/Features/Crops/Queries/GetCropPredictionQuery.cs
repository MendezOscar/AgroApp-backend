using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetCropPredictionQuery(Guid PlotId, Guid CropId) : IRequest<CropPredictionDto?>;
