using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Queries;

public class GetLaborByIdQueryHandler : IRequestHandler<GetLaborByIdQuery, LaborDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetLaborByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<LaborDto?> Handle(GetLaborByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.LaborLogs
            .Where(l => l.Id == request.Id
                     && l.CropId == request.CropId
                     && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .Select(l => new LaborDto(
                l.Id, l.CropId, l.UserId, l.ActivityType,
                l.HoursWorked, l.WorkersCount, l.Cost,
                l.PerformedAt, l.Notes, l.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}