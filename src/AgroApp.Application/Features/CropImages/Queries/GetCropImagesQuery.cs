using AgroApp.Application.Features.CropImages.DTOs;
using MediatR;

namespace AgroApp.Application.Features.CropImages.Queries;

public record GetCropImagesQuery(Guid CropId) : IRequest<List<CropImageDto>>;