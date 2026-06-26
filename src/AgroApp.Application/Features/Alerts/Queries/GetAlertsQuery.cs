using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Alerts.Queries;

public record GetAlertsQuery(
    bool OnlyUnread = false,
    int Page = 1,
    int PageSize = 20
) : IRequest<PagedResult<AlertDto>>;