using MediatR;

namespace AgroApp.Application.Features.Alerts.Commands;

public record GenerateIrrigationRemindersCommand(
    int ThresholdDays = 7,
    decimal SoilHumidityThresholdPct = 30m
) : IRequest<int>;
