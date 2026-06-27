namespace AgroApp.Application.Features.Alerts.DTOs;

public record AlertDto(
    Guid Id,
    Guid? DeviceId,
    Guid? PlotId,
    Guid? CropId,
    string AlertType,
    string Severity,
    string Message,
    bool IsRead,
    DateTime TriggeredAt,
    DateTime? ReadAt
);

public record AlertRuleDto(
    Guid Id,
    Guid TenantId,
    Guid? PlotId,
    string Metric,
    string Operator,
    decimal Threshold,
    string Severity,
    bool IsActive
);

public record CreateAlertRuleRequest(
    Guid? PlotId,
    string Metric,
    string Operator,
    decimal Threshold,
    string Severity
);

public record UpdateAlertRuleRequest(
    Guid? PlotId,
    string Metric,
    string Operator,
    decimal Threshold,
    string Severity,
    bool IsActive
);