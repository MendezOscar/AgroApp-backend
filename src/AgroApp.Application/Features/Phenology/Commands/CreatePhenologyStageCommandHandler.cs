using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Phenology.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Phenology.Commands;

public class CreatePhenologyStageCommandHandler
    : IRequestHandler<CreatePhenologyStageCommand, PhenologyStageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public CreatePhenologyStageCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<PhenologyStageDto> Handle(
        CreatePhenologyStageCommand request,
        CancellationToken cancellationToken)
    {
        // Cerrar etapa anterior si existe
        var previousStage = await _context.PhenologyStages
            .Where(s =>
                s.CropId == request.CropId &&
                s.TenantId == _currentUser.TenantId &&
                s.EndedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (previousStage != null)
        {
            previousStage.EndedAt = request.StartedAt.AddDays(-1);
            previousStage.UpdatedAt = DateTime.UtcNow;
        }

        // Crear nueva etapa
        var stage = new PhenologyStage
        {
            CropId = request.CropId,
            TenantId = _currentUser.TenantId,
            TemplateId = request.TemplateId,
            StageName = request.StageName,
            StageOrder = request.StageOrder,
            StartedAt = request.StartedAt,
            Observations = request.Observations,
            IsCustom = request.IsCustom,
        };

        _context.PhenologyStages.Add(stage);
        await _context.SaveChangesAsync(cancellationToken);

        // Obtener template para el ícono
        var template = request.TemplateId.HasValue
            ? await _context.PhenologyTemplates
                .FirstOrDefaultAsync(
                    t => t.Id == request.TemplateId.Value,
                    cancellationToken)
            : null;

        // Notificar al tenant
        await _notifications.SendToTenantAsync(
            _currentUser.TenantId,
            title: $"{template?.Icon ?? "🌱"} Nueva etapa fenológica",
            body: $"El cultivo ingresó a: {request.StageName}",
            data: new Dictionary<string, string>
            {
                ["cropId"] = request.CropId.ToString(),
                ["type"] = "phenology_stage"
            });

        return new PhenologyStageDto(
            stage.Id, stage.CropId, stage.TemplateId,
            stage.StageName, stage.StageOrder,
            template?.Icon,
            stage.StartedAt, stage.EndedAt,
            stage.Observations, stage.IsCustom,
            true, 0, stage.CreatedAt);
    }
}