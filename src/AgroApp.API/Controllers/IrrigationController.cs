using AgroApp.Application.Features.Irrigation.Commands;
using AgroApp.Application.Features.Irrigation.DTOs;
using AgroApp.Application.Features.Irrigation.Queries;
using AgroApp.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/irrigation")]
public class IrrigationController : ControllerBase
{
    private readonly IMediator _mediator;

    public IrrigationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<PagedResult<IrrigationDto>>> GetAll(
        Guid cropId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(
            new GetIrrigationsQuery(cropId, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [RequireRole(Roles.All)]

    public async Task<ActionResult<IrrigationDto>> GetById(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new GetIrrigationByIdQuery(cropId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<IrrigationDto>> Create(Guid cropId, [FromBody] CreateIrrigationRequest request)
    {
        var command = new CreateIrrigationCommand(
            cropId, request.Method, request.VolumeLiters,
            request.DurationMin, request.AppliedAt, request.Notes, request.TaskId);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { cropId, id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<IrrigationDto>> Update(Guid cropId, Guid id, [FromBody] UpdateIrrigationRequest request)
    {
        var command = new UpdateIrrigationCommand(
            cropId, id, request.Method, request.VolumeLiters,
            request.DurationMin, request.AppliedAt, request.Notes);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<IActionResult> Delete(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new DeleteIrrigationCommand(cropId, id));
        return result ? NoContent() : NotFound();
    }
}