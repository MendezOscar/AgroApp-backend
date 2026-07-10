namespace AgroApp.Application.Features.Sales.DTOs;

public record CropSaleDto(
    Guid Id,
    Guid CropId,
    Guid UserId,
    DateOnly SoldAt,
    decimal QuantityKg,
    decimal PricePerKg,
    decimal TotalAmount,
    string? Buyer,
    string? Notes,
    DateTime CreatedAt
);

public record CreateCropSaleRequest(
    DateOnly SoldAt,
    decimal QuantityKg,
    decimal PricePerKg,
    string? Buyer,
    string? Notes
);
