using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Irrigation.Commands;

public record UpdateIrrigationCommand(
    Guid CropId,
    Guid Id,
    string Method,
    decimal? VolumeLiters,
    int? DurationMin,
    DateTime AppliedAt,
    string? Notes,
    decimal? Cost = null
) : IRequest<IrrigationDto?>;