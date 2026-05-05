using MediatR;

namespace AgroApp.Application.Features.CropImages.Commands;

public record DeleteCropImageCommand(Guid CropId, Guid Id) : IRequest<bool>;