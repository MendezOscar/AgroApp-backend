using AgroApp.Application.Features.Farms.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Farms.Queries;

public record GetFarmsQuery : IRequest<List<FarmDto>>;