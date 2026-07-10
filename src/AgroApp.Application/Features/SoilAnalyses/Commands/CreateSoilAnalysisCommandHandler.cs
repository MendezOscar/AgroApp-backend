using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.SoilAnalyses.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.SoilAnalyses.Commands;

public class CreateSoilAnalysisCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<CreateSoilAnalysisCommand, SoilAnalysisDto>
{
    public async Task<SoilAnalysisDto> Handle(
        CreateSoilAnalysisCommand request,
        CancellationToken cancellationToken)
    {
        var plotExists = await context.Plots.AnyAsync(p =>
            p.Id == request.PlotId && p.Farm.TenantId == currentUser.TenantId,
            cancellationToken);

        if (!plotExists)
            throw new InvalidOperationException("Parcela no encontrada.");

        var analysis = new SoilAnalysis
        {
            PlotId = request.PlotId,
            AnalyzedAt = request.AnalyzedAt,
            Ph = request.Ph,
            NitrogenPct = request.NitrogenPct,
            PhosphorusPct = request.PhosphorusPct,
            PotassiumPct = request.PotassiumPct,
            OrganicMatterPct = request.OrganicMatterPct,
            Notes = request.Notes,
        };

        context.SoilAnalyses.Add(analysis);
        await context.SaveChangesAsync(cancellationToken);

        return new SoilAnalysisDto(
            analysis.Id, analysis.PlotId, analysis.AnalyzedAt,
            analysis.Ph, analysis.NitrogenPct, analysis.PhosphorusPct,
            analysis.PotassiumPct, analysis.OrganicMatterPct,
            analysis.Notes, analysis.CreatedAt);
    }
}
