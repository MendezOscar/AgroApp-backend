using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Sales.Commands;
using AgroApp.Application.Features.Sales.DTOs;
using AgroApp.Application.Features.Sales.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/sales")]
public class CropSalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropSalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<CropSaleDto>>> GetAll(Guid cropId)
    {
        var result = await _mediator.Send(new GetCropSalesQuery(cropId));
        return Ok(result);
    }

    [HttpPost]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<CropSaleDto>> Create(
        Guid cropId, [FromBody] CreateCropSaleRequest request)
    {
        var result = await _mediator.Send(new CreateCropSaleCommand(
            cropId, request.SoldAt, request.QuantityKg,
            request.PricePerKg, request.Buyer, request.Notes));
        return Ok(result);
    }
}
