using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Labor.Queries;

public record GetLaborsQuery(
    Guid CropId,
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResult<LaborDto>>;