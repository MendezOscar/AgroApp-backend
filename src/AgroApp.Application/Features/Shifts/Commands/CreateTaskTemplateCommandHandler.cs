using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Shifts.DTOs;
using AgroApp.Application.Features.Shifts.Services;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;

namespace AgroApp.Application.Features.Shifts.Commands;

public class CreateTaskTemplateCommandHandler
    : IRequestHandler<CreateTaskTemplateCommand, TaskTemplateDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateTaskTemplateCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<TaskTemplateDto> Handle(
        CreateTaskTemplateCommand request,
        CancellationToken cancellationToken)
    {
        if (request.RequiredPhenologyStage is not null && request.CropId is null)
            throw new InvalidOperationException(
                "Solo se puede exigir una etapa fenológica si el turno está ligado a un cultivo.");

        var template = new TaskTemplate
        {
            TenantId = _currentUser.TenantId,
            CreatedBy = _currentUser.UserId,
            PlotId = request.PlotId,
            CropId = request.CropId,
            Title = request.Title,
            Description = request.Description,
            TaskType = Enum.Parse<TaskType>(request.TaskType, true),
            Priority = Enum.Parse<TaskPriority>(request.Priority, true),
            Shift = Enum.Parse<ShiftType>(request.Shift, true),
            RecurrenceType = Enum.Parse<RecurrenceType>(
                request.RecurrenceType, true),
            WeekDays = request.WeekDays,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            RequiredPhenologyStage = request.RequiredPhenologyStage,
        };

        _context.TaskTemplates.Add(template);

        // Generar ocurrencias automáticamente
        var occurrences = OccurrenceGeneratorService.Generate(
            template, _currentUser.TenantId);
        _context.TaskOccurrences.AddRange(occurrences);

        await _context.SaveChangesAsync(cancellationToken);

        return new TaskTemplateDto(
            template.Id, template.CreatedBy, string.Empty,
            template.PlotId, null, template.CropId, null,
            template.Title, template.Description,
            template.TaskType.ToString(), template.Priority.ToString(),
            template.Shift.ToString(), template.RecurrenceType.ToString(),
            template.WeekDays, template.StartDate, template.EndDate,
            template.RequiredPhenologyStage,
            template.IsActive, occurrences.Count, template.CreatedAt);
    }
}