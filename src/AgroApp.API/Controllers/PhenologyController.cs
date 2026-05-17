using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Phenology.Commands;
using AgroApp.Application.Features.Phenology.DTOs;
using AgroApp.Application.Features.Phenology.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/phenology")]
public class PhenologyController : ControllerBase
{
    private readonly IMediator _mediator;
    public PhenologyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<PhenologyStageDto>>> GetStages(Guid cropId)
    {
        var result = await _mediator.Send(new GetPhenologyQuery(cropId));
        return Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<PhenologyStageDto>> Create(
        Guid cropId,
        [FromBody] CreatePhenologyStageRequest request)
    {
        var result = await _mediator.Send(new CreatePhenologyStageCommand(
            cropId, request.TemplateId, request.StageName,
            request.StageOrder, request.StartedAt,
            request.Observations, request.IsCustom));
        return Ok(result);
    }

    [HttpPatch("{stageId}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<PhenologyStageDto>> Update(
        Guid cropId, Guid stageId,
        [FromBody] UpdatePhenologyStageRequest request)
    {
        var result = await _mediator.Send(new UpdatePhenologyStageCommand(
            stageId, cropId, request.EndedAt, request.Observations));
        return result is null ? NotFound() : Ok(result);
    }
}

[ApiController]
[Authorize]
[Route("api/phenology/templates")]
public class PhenologyTemplatesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PhenologyTemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{cropType}")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<PhenologyTemplateDto>>> GetTemplates(
        string cropType)
    {
        var result = await _mediator.Send(new GetTemplatesQuery(cropType));
        return Ok(result);
    }
}