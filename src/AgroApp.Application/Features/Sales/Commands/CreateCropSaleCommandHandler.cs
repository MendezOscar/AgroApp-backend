using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sales.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sales.Commands;

public class CreateCropSaleCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<CreateCropSaleCommand, CropSaleDto>
{
    public async Task<CropSaleDto> Handle(
        CreateCropSaleCommand request,
        CancellationToken cancellationToken)
    {
        var cropExists = await context.Crops.AnyAsync(c =>
            c.Id == request.CropId && c.Plot.Farm.TenantId == currentUser.TenantId,
            cancellationToken);

        if (!cropExists)
            throw new InvalidOperationException("Cultivo no encontrado.");

        var sale = new CropSale
        {
            CropId = request.CropId,
            UserId = currentUser.UserId,
            SoldAt = request.SoldAt,
            QuantityKg = request.QuantityKg,
            PricePerKg = request.PricePerKg,
            Buyer = request.Buyer,
            Notes = request.Notes,
        };

        context.CropSales.Add(sale);
        await context.SaveChangesAsync(cancellationToken);

        return new CropSaleDto(
            sale.Id, sale.CropId, sale.UserId, sale.SoldAt,
            sale.QuantityKg, sale.PricePerKg, sale.QuantityKg * sale.PricePerKg,
            sale.Buyer, sale.Notes, sale.CreatedAt);
    }
}
