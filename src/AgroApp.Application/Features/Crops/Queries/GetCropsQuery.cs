using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetCropsQuery(Guid PlotId) : IRequest<List<CropDto>>;