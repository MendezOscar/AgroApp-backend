using AgroApp.Application.Features.Labor.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Labor.Commands;

public record CreateLaborCommand(
    Guid CropId,
    string ActivityType,
    decimal? HoursWorked,
    int WorkersCount,
    decimal? Cost,
    DateTime PerformedAt,
    string? Notes
) : IRequest<LaborDto>;