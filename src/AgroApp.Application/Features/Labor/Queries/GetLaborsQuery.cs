using AgroApp.Application.Features.Labor.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Labor.Queries;

public record GetLaborsQuery(Guid CropId) : IRequest<List<LaborDto>>;