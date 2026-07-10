using AgroApp.Application.Features.Sales.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sales.Commands;

public record CreateCropSaleCommand(
    Guid CropId,
    DateOnly SoldAt,
    decimal QuantityKg,
    decimal PricePerKg,
    string? Buyer,
    string? Notes
) : IRequest<CropSaleDto>;
