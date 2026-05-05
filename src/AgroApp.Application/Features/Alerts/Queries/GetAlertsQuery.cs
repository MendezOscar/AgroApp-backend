using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Alerts.Queries;

public record GetAlertsQuery(bool OnlyUnread = false) : IRequest<List<AlertDto>>;