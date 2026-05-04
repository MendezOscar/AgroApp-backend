using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Irrigation.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Irrigation.Commands;

public class CreateIrrigationCommandHandler : IRequestHandler<CreateIrrigationCommand, IrrigationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateIrrigationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<IrrigationDto> Handle(CreateIrrigationCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.CropId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null)
            throw new InvalidOperationException("Cultivo no encontrado.");

        var irrigation = new IrrigationLog
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            Method = request.Method,
            VolumeLiters = request.VolumeLiters,
            DurationMin = request.DurationMin,
            AppliedAt = request.AppliedAt,
            Notes = request.Notes
        };

        _context.IrrigationLogs.Add(irrigation);
        await _context.SaveChangesAsync(cancellationToken);

        return new IrrigationDto(
            irrigation.Id, irrigation.CropId, irrigation.UserId,
            irrigation.Method, irrigation.VolumeLiters, irrigation.DurationMin,
            irrigation.AppliedAt, irrigation.Notes, irrigation.CreatedAt);
    }
}