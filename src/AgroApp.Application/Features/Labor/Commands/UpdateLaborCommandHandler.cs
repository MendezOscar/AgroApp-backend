using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Commands;

public class UpdateLaborCommandHandler : IRequestHandler<UpdateLaborCommand, LaborDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateLaborCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<LaborDto?> Handle(UpdateLaborCommand request, CancellationToken cancellationToken)
    {
        var labor = await _context.LaborLogs
            .Include(l => l.Crop.Plot.Farm)
            .FirstOrDefaultAsync(l => l.Id == request.Id
                                   && l.CropId == request.CropId
                                   && l.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (labor is null) return null;

        labor.ActivityType = request.ActivityType;
        labor.HoursWorked = request.HoursWorked;
        labor.WorkersCount = request.WorkersCount;
        labor.Cost = request.Cost;
        labor.PerformedAt = request.PerformedAt;
        labor.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new LaborDto(
            labor.Id, labor.CropId, labor.UserId, labor.ActivityType,
            labor.HoursWorked, labor.WorkersCount, labor.Cost,
            labor.PerformedAt, labor.Notes, labor.CreatedAt);
    }
}