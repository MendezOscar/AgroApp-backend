using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Phenology.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Phenology.Queries;

public class GetPhenologyQueryHandler
    : IRequestHandler<GetPhenologyQuery, List<PhenologyStageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetPhenologyQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<PhenologyStageDto>> Handle(
        GetPhenologyQuery request,
        CancellationToken cancellationToken)
    {
        var stages = await _context.PhenologyStages
            .Include(s => s.Template)
            .Where(s =>
                s.CropId == request.CropId &&
                s.TenantId == _currentUser.TenantId)
            .OrderBy(s => s.StageOrder)
            .ToListAsync(cancellationToken);

        return stages.Select(s => new PhenologyStageDto(
            s.Id, s.CropId, s.TemplateId,
            s.StageName,
            s.StageOrder,
            s.Template?.Icon,
            s.StartedAt,
            s.EndedAt,
            s.Observations,
            s.IsCustom,
            s.EndedAt == null,
            s.EndedAt.HasValue
                ? s.EndedAt.Value.DayNumber - s.StartedAt.DayNumber
                : DateOnly.FromDateTime(DateTime.UtcNow).DayNumber
                  - s.StartedAt.DayNumber,
            s.CreatedAt
        )).ToList();
    }
}

public class GetTemplatesQueryHandler
    : IRequestHandler<GetTemplatesQuery, List<PhenologyTemplateDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTemplatesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PhenologyTemplateDto>> Handle(
        GetTemplatesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.PhenologyTemplates
            .Where(t => t.CropType == request.CropType.ToLower())
            .OrderBy(t => t.StageOrder)
            .Select(t => new PhenologyTemplateDto(
                t.Id, t.CropType, t.StageName,
                t.StageOrder, t.Description,
                t.MinDays, t.MaxDays,
                t.Icon, t.Recommendations))
            .ToListAsync(cancellationToken);
    }
}