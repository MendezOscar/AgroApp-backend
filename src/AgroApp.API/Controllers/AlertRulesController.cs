using AgroApp.Application.Features.Alerts.Commands;
using AgroApp.Application.Features.Alerts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/alert-rules")]
public class AlertRulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AlertRulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AlertRuleDto>> Create([FromBody] CreateAlertRuleRequest request)
    {
        var command = new CreateAlertRuleCommand(
            request.PlotId, request.Metric,
            request.Operator, request.Threshold, request.Severity);

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}