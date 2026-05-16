using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Tasks.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public DeleteTaskCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        DeleteTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t =>
                t.Id == request.Id &&
                t.TenantId == _currentUser.TenantId &&
                t.CreatedBy == _currentUser.UserId,
                cancellationToken);

        if (task is null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}