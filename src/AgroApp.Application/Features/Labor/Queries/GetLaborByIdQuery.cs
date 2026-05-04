using AgroApp.Application.Features.Labor.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Labor.Queries;

public record GetLaborByIdQuery(Guid CropId, Guid Id) : IRequest<LaborDto?>;