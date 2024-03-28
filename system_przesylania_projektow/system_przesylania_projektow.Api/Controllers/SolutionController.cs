using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.SolutionRequests.AddSolution;
using system_przesylania_projektow.Application.Requests.SolutionRequests.DeleteSolution;
using system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadSolution;

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
    public async Task<IActionResult> DeleteSolution([FromRoute] Guid solutionId) {
        var request = new DeleteSolutionRequest() {
            SolutionId = solutionId,
        };
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet("{solutionId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DownloadSolution([FromRoute] Guid solutionId) {
        var request = new DownloadSolutionRequest()
        {
            SolutionId = solutionId,
        };

        var solution = await _mediator.Send(request);


        return Ok(solution);
    }

    [HttpGet("all-{projectId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DownloadAllSolutions([FromRoute] Guid projectId) {
        return Ok();
    }
}
