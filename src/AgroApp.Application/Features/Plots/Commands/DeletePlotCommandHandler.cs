using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Plots.Commands;

public class DeletePlotCommandHandler : IRequestHandler<DeletePlotCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeletePlotCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeletePlotCommand request, CancellationToken cancellationToken)
    {
        var plot = await _context.Plots
            .Include(p => p.Farm)
            .FirstOrDefaultAsync(p => p.Id == request.Id
                                   && p.FarmId == request.FarmId
                                   && p.Farm.TenantId == _currentUser.TenantId
                                   && p.IsActive, cancellationToken);

        if (plot is null) return false;

        plot.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}