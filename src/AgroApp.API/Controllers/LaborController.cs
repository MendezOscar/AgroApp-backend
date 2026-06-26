using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Labor.Commands;
using AgroApp.Application.Features.Labor.DTOs;
using AgroApp.Application.Features.Labor.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/labor")]
public class LaborController : ControllerBase
{
    private readonly IMediator _mediator;

    public LaborController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<LaborDto>>> GetAll(
        Guid cropId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(
            new GetLaborsQuery(cropId, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LaborDto>> GetById(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new GetLaborByIdQuery(cropId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<LaborDto>> Create(Guid cropId, [FromBody] CreateLaborRequest request)
    {
        var command = new CreateLaborCommand(
            cropId, request.ActivityType, request.HoursWorked,
            request.WorkersCount, request.Cost,
            request.PerformedAt, request.Notes);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { cropId, id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LaborDto>> Update(Guid cropId, Guid id, [FromBody] UpdateLaborRequest request)
    {
        var command = new UpdateLaborCommand(
            cropId, id, request.ActivityType, request.HoursWorked,
            request.WorkersCount, request.Cost,
            request.PerformedAt, request.Notes);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new DeleteLaborCommand(cropId, id));
        return result ? NoContent() : NotFound();
    }
}