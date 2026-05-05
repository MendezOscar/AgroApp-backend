using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Alerts.Commands;

public record CreateAlertRuleCommand(
    Guid? PlotId,
    string Metric,
    string Operator,
    decimal Threshold,
    string Severity
) : IRequest<AlertRuleDto>;