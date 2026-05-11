using AgroApp.Application.Features.Notifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("fcm-token")]
    public async Task<IActionResult> RegisterToken(
        [FromBody] RegisterTokenRequest request)
    {
        await _mediator.Send(
            new RegisterFcmTokenCommand(request.Token, request.Platform));
        return Ok();
    }
}

public record RegisterTokenRequest(string Token, string Platform = "mobile");