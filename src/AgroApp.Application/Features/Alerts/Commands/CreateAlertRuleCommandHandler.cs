using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Alerts.DTOs;
using AgroApp.Domain.Entities;
using MediatR;

namespace AgroApp.Application.Features.Alerts.Commands;

public class CreateAlertRuleCommandHandler : IRequestHandler<CreateAlertRuleCommand, AlertRuleDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateAlertRuleCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<AlertRuleDto> Handle(CreateAlertRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = new AlertRule
        {
            TenantId = _currentUser.TenantId,
            PlotId = request.PlotId,
            Metric = request.Metric,
            Operator = request.Operator,
            Threshold = request.Threshold,
            Severity = request.Severity
        };

        _context.AlertRules.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);

        return new AlertRuleDto(
            rule.Id, rule.TenantId, rule.PlotId, rule.Metric,
            rule.Operator, rule.Threshold, rule.Severity, rule.IsActive);
    }
}