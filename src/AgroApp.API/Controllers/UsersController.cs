using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("invite")]
    [RequireRole(Roles.Admin)]
    public async Task<IActionResult> Invite([FromBody] InviteUserRequest request)
    {
        await _mediator.Send(new InviteUserCommand(
            request.Name,
            request.Email,
            request.Password,
            request.Role));
        return Ok(new { message = "Usuario invitado correctamente" });
    }
}

public record InviteUserRequest(
    string Name,
    string Email,
    string Password,
    string Role
);