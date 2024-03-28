using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.TaskRequests.CreateTask;
using system_przesylania_projektow.Application.Requests.TaskRequests.DeleteTask;

namespace system_przesylania_projektow.Api.Controllers;

[Route("api/task")]
[ApiController]
public class TaskController : ControllerBase {

    private readonly IMediator _mediator;

    public TaskController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{taskId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId) {
        var request = new DeleteTaskRequest()
        {
            TaskId = taskId
        };
        await _mediator.Send(request);
        return Ok();
    }
}
