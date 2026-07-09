using AgroApp.Application.Features.Crops.Commands;
using AgroApp.Application.Features.Crops.DTOs;
using AgroApp.Application.Features.Crops.Queries;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/plots/{plotId}/crops")]
public class CropsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<CropDto>>> GetAll(Guid plotId, [FromQuery] CropStatus? status = null)
    {
        var result = await _mediator.Send(new GetCropsQuery(plotId, status));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<CropDto>> GetById(Guid plotId, Guid id)
    {
        var result = await _mediator.Send(new GetCropByIdQuery(plotId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id}/prediction")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<CropPredictionDto>> GetPrediction(Guid plotId, Guid id)
    {
        var result = await _mediator.Send(new GetCropPredictionQuery(plotId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<CropDto>> Create(Guid plotId, [FromBody] CreateCropRequest request)
    {
        var command = new CreateCropCommand(
            plotId, request.CropType, request.Variety,
            request.PlantedAt, request.EstimatedHarvest, request.Notes);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { plotId, id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<CropDto>> Update(Guid plotId, Guid id, [FromBody] UpdateCropRequest request)
    {
        var command = new UpdateCropCommand(
            plotId, id, request.CropType, request.Variety,
            request.PlantedAt, request.EstimatedHarvest, request.HarvestedAt,
            request.Status, request.YieldKg, request.Notes);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<IActionResult> Delete(Guid plotId, Guid id)
    {
        var result = await _mediator.Send(new DeleteCropCommand(plotId, id));
        return result ? NoContent() : NotFound();
    }
}