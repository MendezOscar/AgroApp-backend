using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Sales.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Sales.Queries;

public class GetCropSalesQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUser)
        : IRequestHandler<GetCropSalesQuery, List<CropSaleDto>>
{
    public async Task<List<CropSaleDto>> Handle(
        GetCropSalesQuery request,
        CancellationToken cancellationToken)
    {
        return await context.CropSales
            .Where(s => s.CropId == request.CropId
                     && s.Crop.Plot.Farm.TenantId == currentUser.TenantId)
            .OrderByDescending(s => s.SoldAt)
            .Select(s => new CropSaleDto(
                s.Id, s.CropId, s.UserId, s.SoldAt,
                s.QuantityKg, s.PricePerKg, s.QuantityKg * s.PricePerKg,
                s.Buyer, s.Notes, s.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
