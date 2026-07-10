using MediatR;

namespace AgroApp.Application.Features.Alerts.Commands;

public record GenerateWeatherAlertsCommand(
    decimal FrostThresholdC = 2m,
    decimal RainThresholdMm = 10m
) : IRequest<int>;
