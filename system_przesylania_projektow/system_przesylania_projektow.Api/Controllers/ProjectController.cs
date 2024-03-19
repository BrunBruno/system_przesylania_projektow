

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace system_przesylania_projektow.Api.Controllers;

[ApiController]
public class ProjectController : ControllerBase {
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator) {
        _mediator = mediator;
    }
}
