using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Application.Features.Crops.Queries;
using AgroApp.Application.Features.Farms.Commands;
using AgroApp.Application.Features.Farms.DTOs;
using AgroApp.Application.Features.Farms.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FarmsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FarmsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<FarmDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetFarmsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<FarmDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetFarmByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<ActionResult<FarmDto>> Create([FromBody] CreateFarmRequest request)
    {
        var command = new CreateFarmCommand(
            request.Name, request.Description,
            request.Lat, request.Lng, request.AreaHa,
            request.Country, request.Region);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<ActionResult<FarmDto>> Update(Guid id, [FromBody] UpdateFarmRequest request)
    {
        var command = new UpdateFarmCommand(
            id, request.Name, request.Description,
            request.Lat, request.Lng, request.AreaHa,
            request.Country, request.Region);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteFarmCommand(id));
        return result ? NoContent() : NotFound();
    }

    [HttpGet("{id}/summary")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<FarmSummaryDto>> GetSummary(Guid id)
    {
        var result = await _mediator.Send(new GetFarmSummaryQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id}/yield-history")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<YieldHistoryDto>>> GetYieldHistory(Guid id, [FromQuery] int months = 12)
    {
        var result = await _mediator.Send(new GetYieldHistoryQuery(id, months));
        return Ok(result);
    }
}