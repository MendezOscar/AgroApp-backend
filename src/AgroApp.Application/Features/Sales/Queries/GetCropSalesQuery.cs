using AgroApp.Application.Features.Sales.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Sales.Queries;

public record GetCropSalesQuery(Guid CropId) : IRequest<List<CropSaleDto>>;
