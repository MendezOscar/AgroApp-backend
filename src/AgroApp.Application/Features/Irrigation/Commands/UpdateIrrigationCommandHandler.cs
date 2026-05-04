using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Commands;

public class UpdateIrrigationCommandHandler : IRequestHandler<UpdateIrrigationCommand, IrrigationDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateIrrigationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<IrrigationDto?> Handle(UpdateIrrigationCommand request, CancellationToken cancellationToken)
    {
        var irrigation = await _context.IrrigationLogs
            .Include(i => i.Crop.Plot.Farm)
            .FirstOrDefaultAsync(i => i.Id == request.Id
                                   && i.CropId == request.CropId
                                   && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (irrigation is null) return null;

        irrigation.Method = request.Method; 
        irrigation.VolumeLiters = request.VolumeLiters;
        irrigation.DurationMin = request.DurationMin;
        irrigation.AppliedAt = request.AppliedAt;
        irrigation.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new IrrigationDto(
            irrigation.Id, irrigation.CropId, irrigation.UserId,
            irrigation.Method, irrigation.VolumeLiters, irrigation.DurationMin,
            irrigation.AppliedAt, irrigation.Notes, irrigation.CreatedAt);
    }
}