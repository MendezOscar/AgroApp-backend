using AgroApp.Application.Features.Farms.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Farms.Queries;

public record GetFarmByIdQuery(Guid Id) : IRequest<FarmDto?>;