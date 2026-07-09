using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Domain.Enums;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetCropsQuery(Guid PlotId, CropStatus? Status = null) : IRequest<List<CropDto>>;