using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetUsersQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.TenantId == _currentUser.TenantId)
            .OrderBy(u => u.Name)
            .Select(u => new UserDto(
                u.Id, u.Name, u.Email,
                u.Role.ToString(), u.IsActive,
                u.LastLoginAt, u.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}