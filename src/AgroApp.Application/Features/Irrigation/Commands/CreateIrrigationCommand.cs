using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Commands;

public record CreateIrrigationCommand(
    Guid CropId,
    string Method,
    decimal? VolumeLiters,
    int? DurationMin,
    DateTime AppliedAt,
    string? Notes,
    decimal? Cost = null,
    Guid? TaskId = null,
    Guid? OccurrenceId = null
) : IRequest<IrrigationDto>;