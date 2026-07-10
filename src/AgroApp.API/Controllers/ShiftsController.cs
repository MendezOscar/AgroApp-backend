using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Shifts.Commands;
using AgroApp.Application.Features.Shifts.DTOs;
using AgroApp.Application.Features.Shifts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/shifts")]
public class ShiftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Templates
    [HttpGet("templates")]
    [RequireRole(Roles.AdminOrManager)]
    public async Task<ActionResult<List<TaskTemplateDto>>> GetTemplates()
    {
        var result = await _mediator.Send(new GetTaskTemplatesQuery());
        return Ok(result);
    }

    [HttpPost("templates")]
    [RequireRole(Roles.AdminOrManager)]
    public async Task<ActionResult<TaskTemplateDto>> CreateTemplate(
        [FromBody] CreateTaskTemplateRequest request)
    {
        var result = await _mediator.Send(new CreateTaskTemplateCommand(
            request.PlotId, request.CropId,
            request.Title, request.Description,
            request.TaskType, request.Priority,
            request.Shift, request.RecurrenceType,
            request.WeekDays, request.StartDate, request.EndDate,
            request.RequiredPhenologyStage));
        return Ok(result);
    }

    // Occurrences
    [HttpGet("occurrences")]
    public async Task<ActionResult<List<TaskOccurrenceDto>>> GetOccurrences(
        [FromQuery] DateOnly? date = null,
        [FromQuery] bool onlyMine = false)
    {
        var result = await _mediator.Send(
            new GetOccurrencesQuery(date, onlyMine));
        return Ok(result);
    }

    [HttpPatch("occurrences/{id}/assign")]
    [RequireRole(Roles.AdminOrManager)]
    public async Task<ActionResult<TaskOccurrenceDto>> Assign(
        Guid id, [FromBody] AssignOccurrenceRequest request)
    {
        var result = await _mediator.Send(new AssignOccurrenceCommand(
            id, request.AssignedTo, request.Shift));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPatch("occurrences/{id}/status")]
    public async Task<ActionResult<TaskOccurrenceDto>> UpdateStatus(
        Guid id, [FromBody] UpdateOccurrenceStatusRequest request)
    {
        var result = await _mediator.Send(new UpdateOccurrenceStatusCommand(
            id, request.Status, request.Notes));
        return result is null ? NotFound() : Ok(result);
    }
}