using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.SoilAnalyses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.SoilAnalyses.Queries;

public class GetSoilAnalysesQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetSoilAnalysesQuery, List<SoilAnalysisDto>>
{
    public async Task<List<SoilAnalysisDto>> Handle(
        GetSoilAnalysesQuery request,
        CancellationToken cancellationToken)
    {
        return await context.SoilAnalyses
            .Where(s => s.PlotId == request.PlotId
                     && s.Plot.Farm.TenantId == currentUser.TenantId)
            .OrderByDescending(s => s.AnalyzedAt)
            .Select(s => new SoilAnalysisDto(
                s.Id, s.PlotId, s.AnalyzedAt, s.Ph, s.NitrogenPct,
                s.PhosphorusPct, s.PotassiumPct, s.OrganicMatterPct,
                s.Notes, s.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
