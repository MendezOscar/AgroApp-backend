using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Alerts.Commands;
using AgroApp.Application.Features.Alerts.DTOs;
using AgroApp.Application.Features.Alerts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/alerts")]
public class AlertsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AlertsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AlertDto>>> GetAll(
        [FromQuery] bool onlyUnread = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetAlertsQuery(onlyUnread, page, pageSize));
        return Ok(result);
    }

    [HttpGet("unread-count")]
    public async Task<ActionResult<int>> GetUnreadCount()
    {
        var result = await _mediator.Send(new GetUnreadAlertCountQuery());
        return Ok(result);
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await _mediator.Send(new MarkAlertAsReadCommand(id));
        return result ? NoContent() : NotFound();
    }
}