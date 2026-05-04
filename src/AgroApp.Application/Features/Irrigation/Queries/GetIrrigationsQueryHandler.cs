using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Queries;

public class GetIrrigationsQueryHandler : IRequestHandler<GetIrrigationsQuery, List<IrrigationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetIrrigationsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<IrrigationDto>> Handle(GetIrrigationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.IrrigationLogs
            .Where(i => i.CropId == request.CropId
                     && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(i => i.AppliedAt)
            .Select(i => new IrrigationDto(
                i.Id, i.CropId, i.UserId, i.Method,
                i.VolumeLiters, i.DurationMin,
                i.AppliedAt, i.Notes, i.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}