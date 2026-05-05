using MediatR;

namespace AgroApp.Application.Features.Alerts.Queries;

public record GetUnreadAlertCountQuery : IRequest<int>;