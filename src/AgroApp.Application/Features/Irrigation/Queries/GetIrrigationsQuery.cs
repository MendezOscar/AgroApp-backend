using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Queries;

public record GetIrrigationsQuery(Guid CropId) : IRequest<List<IrrigationDto>>;