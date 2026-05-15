using AgroApp.Application.Features.CropImages.Commands;
using AgroApp.Application.Features.CropImages.DTOs;
using AgroApp.Application.Features.CropImages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/crops/{cropId}/images")]
public class CropImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [RequireRole(Roles.All)]
    public async Task<ActionResult<List<CropImageDto>>> GetAll(Guid cropId)
    {
        var result = await _mediator.Send(new GetCropImagesQuery(cropId));
        return Ok(result);
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10MB máximo
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<ActionResult<CropImageDto>> Upload(
        Guid cropId,
        IFormFile file,
        [FromForm] string? category,
        [FromForm] DateTime? takenAt)
    {
        if (file.Length == 0)
            return BadRequest("El archivo está vacío.");

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType))
            return BadRequest("Solo se permiten imágenes JPG, PNG o WebP.");

        using var stream = file.OpenReadStream();

        var command = new UploadCropImageCommand(
            cropId,
            stream,
            file.FileName,
            file.ContentType,
            category,
            takenAt);

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [RequireRole(Roles.AdminManagerOrFarmer)]
    public async Task<IActionResult> Delete(Guid cropId, Guid id)
    {
        var result = await _mediator.Send(new DeleteCropImageCommand(cropId, id));
        return result ? NoContent() : NotFound();
    }
}