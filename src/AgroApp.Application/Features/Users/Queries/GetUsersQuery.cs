using AgroApp.Application.Features.Users.DTOs;
using MediatR;

namespace AgroApp.Application.Features.Users.Queries;

public record GetUsersQuery : IRequest<List<UserDto>>;