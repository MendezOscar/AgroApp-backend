using AgroApp.Application.Features.Fertilization.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Fertilization.Queries;

public record GetFertilizationsQuery(Guid CropId) : IRequest<List<FertilizationDto>>;