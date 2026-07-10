using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Phenology.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Phenology.Commands;

public class UpdatePhenologyStageCommandHandler
    : IRequestHandler<UpdatePhenologyStageCommand, PhenologyStageDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdatePhenologyStageCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PhenologyStageDto?> Handle(
        UpdatePhenologyStageCommand request,
        CancellationToken cancellationToken)
    {
        var stage = await _context.PhenologyStages
            .Include(s => s.Template)
            .FirstOrDefaultAsync(s =>
                s.Id == request.StageId &&
                s.CropId == request.CropId &&
                s.TenantId == _currentUser.TenantId,
                cancellationToken);

        if (stage is null) return null;

        if (request.EndedAt.HasValue)
            stage.EndedAt = request.EndedAt;
        if (request.Observations != null)
            stage.Observations = request.Observations;
        stage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new PhenologyStageDto(
            stage.Id, stage.CropId, stage.TemplateId,
            stage.StageName, stage.StageOrder,
            stage.Template?.Icon, stage.Template?.Recommendations,
            stage.StartedAt, stage.EndedAt,
            stage.Observations, stage.IsCustom,
            stage.EndedAt == null,
            stage.EndedAt.HasValue
                ? stage.EndedAt.Value.DayNumber - stage.StartedAt.DayNumber
                : DateOnly.FromDateTime(DateTime.UtcNow).DayNumber
                  - stage.StartedAt.DayNumber,
            stage.CreatedAt);
    }
}