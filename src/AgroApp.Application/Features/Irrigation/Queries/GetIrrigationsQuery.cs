using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Queries;

public record GetIrrigationsQuery(
    Guid CropId,
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResult<IrrigationDto>>;