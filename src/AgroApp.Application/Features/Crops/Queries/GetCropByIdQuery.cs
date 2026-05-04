using AgroApp.Application.Features.Crops.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Crops.Queries;

public record GetCropByIdQuery(Guid PlotId, Guid Id) : IRequest<CropDto?>;