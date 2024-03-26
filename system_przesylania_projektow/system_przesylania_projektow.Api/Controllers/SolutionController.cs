using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.SolutionRequests.AddSolution;
using system_przesylania_projektow.Application.Requests.SolutionRequests.DeleteSolution;

namespace system_przesylania_projektow.Api.Controllers; 
[Route("api/solution")]
[ApiController]
public class SolutionController : ControllerBase {
    private readonly IMediator _mediator;

    public SolutionController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> AddSolution(AddSolutionRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{solutionId}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> DeleteSolution(DeleteSolutionRequest request) {
        await _mediator.Send(request);
        return Ok();
    }
}
