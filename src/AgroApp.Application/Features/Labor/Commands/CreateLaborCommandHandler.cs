using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Labor.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Labor.Commands;

public class CreateLaborCommandHandler : IRequestHandler<CreateLaborCommand, LaborDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateLaborCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<LaborDto> Handle(CreateLaborCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.CropId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null)
            throw new InvalidOperationException("Cultivo no encontrado.");

        var labor = new LaborLog
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            ActivityType = request.ActivityType,
            HoursWorked = request.HoursWorked,
            WorkersCount = request.WorkersCount,
            Cost = request.Cost,
            PerformedAt = request.PerformedAt,
            Notes = request.Notes
        };

        _context.LaborLogs.Add(labor);
        await _context.SaveChangesAsync(cancellationToken);

        return new LaborDto(
            labor.Id, labor.CropId, labor.UserId, labor.ActivityType,
            labor.HoursWorked, labor.WorkersCount, labor.Cost,
            labor.PerformedAt, labor.Notes, labor.CreatedAt);
    }
}