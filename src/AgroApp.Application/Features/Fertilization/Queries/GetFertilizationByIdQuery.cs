using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Queries;

public record GetFertilizationByIdQuery(Guid CropId, Guid Id) : IRequest<FertilizationDto?>;