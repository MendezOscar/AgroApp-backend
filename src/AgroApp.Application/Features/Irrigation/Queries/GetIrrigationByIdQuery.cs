using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Queries;

public record GetIrrigationByIdQuery(Guid CropId, Guid Id) : IRequest<IrrigationDto?>;