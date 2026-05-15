using AgroApp.API.Authorization;
using AgroApp.Application.Common.Constants;
using AgroApp.Application.Features.Users.Commands;
using AgroApp.Application.Features.Users.Queries;
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

    [HttpGet]
    [RequireRole(Roles.Admin)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetUsersQuery());
        return Ok(result);
    }

    [HttpPost("invite")]
    [RequireRole(Roles.Admin)]
    public async Task<IActionResult> Invite(
        [FromBody] InviteUserRequest request)
    {
        await _mediator.Send(new InviteUserCommand(
            request.Name, request.Email,
            request.Password, request.Role));
        return Ok(new { message = "Usuario invitado correctamente" });
    }

    [HttpPatch("{id}/toggle")]
    [RequireRole(Roles.Admin)]
    public async Task<IActionResult> Toggle(
        Guid id, [FromBody] ToggleUserRequest request)
    {
        var result = await _mediator.Send(
            new ToggleUserCommand(id, request.IsActive));
        return result ? Ok() : NotFound();
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request)
    {
        await _mediator.Send(new ChangePasswordCommand(
            request.CurrentPassword, request.NewPassword));
        return Ok(new { message = "Contraseña actualizada" });
    }
}

public record InviteUserRequest(
    string Name, string Email,
    string Password, string Role);

public record ToggleUserRequest(bool IsActive);

public record ChangePasswordRequest(
    string CurrentPassword, string NewPassword);