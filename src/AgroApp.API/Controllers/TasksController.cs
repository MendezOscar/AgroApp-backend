using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Tasks.Commands;
using AgroApp.Application.Features.Tasks.DTOs;
using AgroApp.Application.Features.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Todos ven tareas — Admin/Manager ven todas, Farmer solo las suyas
    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAll(
        [FromQuery] bool onlyMine = false,
        [FromQuery] string? status = null)
    {
        var result = await _mediator.Send(
            new GetTasksQuery(onlyMine, status));
        return Ok(result);
    }

    // Solo Admin y Manager crean tareas
    [HttpPost]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<ActionResult<TaskDto>> Create(
        [FromBody] CreateTaskRequest request)
    {
        var result = await _mediator.Send(new CreateTaskCommand(
            request.AssignedTo, request.PlotId, request.CropId,
            request.Title, request.Description,
            request.Priority, request.TaskType,request.DueDate));
        return Ok(result);
    }

    // Todos pueden actualizar el estado de sus tareas
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<TaskDto>> UpdateStatus(
        Guid id, [FromBody] UpdateTaskStatusRequest request)
    {
        var result = await _mediator.Send(
            new UpdateTaskStatusCommand(id, request.Status, request.Notes));
        return result is null ? NotFound() : Ok(result);
    }

    // Solo Admin y Manager eliminan tareas
    [HttpDelete("{id}")]
    [RequireRole(Roles.Admin, Roles.Manager)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id));
        return result ? NoContent() : NotFound();
    }
}