using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Common.Models;
using AgroApp.Application.Features.Fertilization.Commands;
using AgroApp.Application.Features.Fertilization.DTOs;
using AgroApp.Application.Features.Fertilization.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/fertilization")]
public class FertilizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public FertilizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<PagedResult<FertilizationDto>>> GetAll(
        Guid cropId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(
            new GetFertilizationsQuery(cropId, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<FertilizationDto>> GetById(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new GetFertilizationByIdQuery(cropId, id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<FertilizationDto>> Create(Guid cropId, [FromBody] CreateFertilizationRequest request)
    {
        var command = new CreateFertilizationCommand(
            cropId, request.ProductName, request.ProductType,
            request.DoseKgHa, request.TotalKg, request.Method,
            request.Cost, request.AppliedAt, request.NextApplication,
            request.Notes, request.TaskId, request.OccurrenceId);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { cropId, id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<FertilizationDto>> Update(Guid cropId, Guid id, [FromBody] UpdateFertilizationRequest request)
    {
        var command = new UpdateFertilizationCommand(
            cropId, id, request.ProductName, request.ProductType,
            request.DoseKgHa, request.TotalKg, request.Method,
            request.Cost, request.AppliedAt, request.NextApplication,
            request.Notes);

        var result = await _mediator.Send(command);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<IActionResult> Delete(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new DeleteFertilizationCommand(cropId, id));
        return result ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/cost")]
    [RequireRole(Roles.AdminOrManager)]
    public async Task<ActionResult<FertilizationDto>> SetCost(Guid cropId, Guid id, [FromBody] SetCostRequest request)
    {
        var result = await _mediator.Send(new SetFertilizationCostCommand(cropId, id, request.Cost));
        return result is null ? NotFound() : Ok(result);
    }
}