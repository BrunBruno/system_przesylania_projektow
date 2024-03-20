using MediatR;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.UserRequests.RegisterUser;
using system_przesylania_projektow.Application.Requests.UserRequests.SignIn;

namespace system_przesylania_projektow.Api.Controllers;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var token = await _mediator.Send(request);
        return Ok(token);
    }
}
