using AgroApp.Application.Features.Farms.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Farms.Commands;

public record UpdateFarmCommand(
    Guid Id,
    string Name,
    string? Description,
    double? Lat,
    double? Lng,
    decimal? AreaHa,
    string? Country,
    string? Region
) : IRequest<FarmDto?>;