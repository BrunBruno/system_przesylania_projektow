using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.StudentRequests.AcceptStudent;
using system_przesylania_projektow.Application.Requests.StudentRequests.JoinStudent;
using system_przesylania_projektow.Application.Requests.StudentRequests.RejectStudent;

namespace system_przesylania_projektow.Api.Controllers;

[Route("api/student")]
[ApiController]
public class StudentController : ControllerBase {
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost("join")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> JoinStudent(JoinStudentRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPut("accept")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> AcceptStudent(AcceptStudentRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{studentId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> RejectStudent([FromRoute] Guid studentId) {
        var request = new RejectStudentRequest()
        {
            StudentId = studentId
        };
        await _mediator.Send(request);
        return Ok();
    }
}
