using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Queries;

public record GetFertilizationsQuery(
    Guid CropId,
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResult<FertilizationDto>>;