using AgroApp.Application.Features.Costs.DTOs;
using AgroApp.Application.Features.Costs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/costs")]
public class CostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("monthly-history")]
    public async Task<ActionResult<List<MonthlyCostDto>>> GetMonthlyHistory(
        [FromQuery] int months = 6)
    {
        var result = await _mediator.Send(new GetMonthlyCostHistoryQuery(months));
        return Ok(result);
    }
}
