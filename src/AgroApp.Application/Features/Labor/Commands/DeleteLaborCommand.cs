using MediatR;

namespace AgroApp.Application.Features.Labor.Commands;

public record DeleteLaborCommand(Guid CropId, Guid Id) : IRequest<bool>;