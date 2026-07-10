using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.SoilAnalyses.Commands;
using AgroApp.Application.Features.SoilAnalyses.DTOs;
using AgroApp.Application.Features.SoilAnalyses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/plots/{plotId}/soil-analyses")]
public class SoilAnalysisController : ControllerBase
{
    private readonly IMediator _mediator;

    public SoilAnalysisController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<SoilAnalysisDto>>> GetAll(Guid plotId)
    {
        var result = await _mediator.Send(new GetSoilAnalysesQuery(plotId));
        return Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<SoilAnalysisDto>> Create(
        Guid plotId, [FromBody] CreateSoilAnalysisRequest request)
    {
        var result = await _mediator.Send(new CreateSoilAnalysisCommand(
            plotId, request.AnalyzedAt, request.Ph, request.NitrogenPct,
            request.PhosphorusPct, request.PotassiumPct,
            request.OrganicMatterPct, request.Notes));
        return Ok(result);
    }
}
