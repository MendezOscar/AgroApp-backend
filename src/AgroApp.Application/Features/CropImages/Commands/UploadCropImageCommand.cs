using AgroApp.Application.Features.CropImages.DTOs;
using MediatR;

namespace AgroApp.Application.Features.CropImages.Commands;

public record UploadCropImageCommand(
    Guid CropId,
    Stream FileStream,
    string FileName,
    string ContentType,
    string? Category,
    DateTime? TakenAt
) : IRequest<CropImageDto>;