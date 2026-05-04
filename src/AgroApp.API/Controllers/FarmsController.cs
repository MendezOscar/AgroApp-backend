using AgroApp.Application.Features.Farms.Commands;
using AgroApp.Application.Features.Farms.DTOs;
using AgroApp.Application.Features.Farms.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<FarmDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetFarmsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FarmDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetFarmByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
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
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteFarmCommand(id));
        return result ? NoContent() : NotFound();
    }
}