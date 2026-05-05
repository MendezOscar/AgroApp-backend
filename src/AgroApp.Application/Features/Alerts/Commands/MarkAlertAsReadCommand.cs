using MediatR;

namespace AgroApp.Application.Features.Alerts.Commands;

public record MarkAlertAsReadCommand(Guid Id) : IRequest<bool>;