using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.CropImages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Queries;

public class GetPestDiagnosisSummaryQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetPestDiagnosisSummaryQuery, List<PestDiagnosisSummaryDto>>
{
    public async Task<List<PestDiagnosisSummaryDto>> Handle(
        GetPestDiagnosisSummaryQuery request,
        CancellationToken cancellationToken)
    {
        return await context.CropImages
            .Where(i => i.DiagnosisCondition != null
                     && i.Crop.Plot.FarmId == request.FarmId
                     && i.Crop.Plot.Farm.TenantId == currentUser.TenantId)
            .GroupBy(i => i.DiagnosisCondition!)
            .Select(g => new PestDiagnosisSummaryDto(
                g.Key, g.Count(), g.Max(i => i.AiAnalyzedAt ?? i.CreatedAt)))
            .OrderByDescending(s => s.LastDetectedAt)
            .ToListAsync(cancellationToken);
    }
}
