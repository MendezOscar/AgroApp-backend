using AgroApp.Application.Features.Plots.Commands;
using AgroApp.Application.Features.Plots.DTOs;
using AgroApp.Application.Features.Plots.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/farms/{farmId}/plots")]
public class PlotsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlotDto>>> GetAll(Guid farmId)
    {
        var result = await _mediator.Send(new GetPlotsQuery(farmId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlotDto>> GetById(Guid farmId, Guid id)
    {
        var result = await _mediator.Send(new GetPlotByIdQuery(farmId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PlotDto>> Create(Guid farmId, [FromBody] CreatePlotRequest request)
    {
        var command = new CreatePlotCommand(
            farmId, request.Name, request.SoilType,
            request.AreaHa, request.GeoJson, request.Notes);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { farmId, id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlotDto>> Update(Guid farmId, Guid id, [FromBody] UpdatePlotRequest request)
    {
        var command = new UpdatePlotCommand(
            farmId, id, request.Name, request.SoilType,
            request.AreaHa, request.GeoJson, request.Notes);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid farmId, Guid id)
    {
        var result = await _mediator.Send(new DeletePlotCommand(farmId, id));
        return result ? NoContent() : NotFound();
    }
}