using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Queries;

public class GetIrrigationByIdQueryHandler : IRequestHandler<GetIrrigationByIdQuery, IrrigationDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetIrrigationByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<IrrigationDto?> Handle(GetIrrigationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.IrrigationLogs
            .Where(i => i.Id == request.Id
                     && i.CropId == request.CropId
                     && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(i => new IrrigationDto(
                i.Id, i.CropId, i.UserId, i.Method,
                i.VolumeLiters, i.DurationMin,
                i.AppliedAt, i.Notes, i.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}