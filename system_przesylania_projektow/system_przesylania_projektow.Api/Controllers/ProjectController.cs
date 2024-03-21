using MediatR;
using Microsoft.AspNetCore.Mvc;
using system_przesylania_projektow.Application.Requests.ProjectRequests.CreateProject;
using system_przesylania_projektow.Application.Requests.ProjectRequests.DeleteProject;
using system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects;
using system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject;
using system_przesylania_projektow.Application.Requests.ProjectRequests.UpdateProjec;

namespace system_przesylania_projektow.Api.Controllers;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase {
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects(GetAllProjectsRequest request) {
        var games = await _mediator.Send(request);
        return Ok(games);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProject(GetProjectRequest request) {
        var game = await _mediator.Send(request);
        return Ok(game);
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> UpdateProject(UpdateProjectRequest request) {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> DeleteProject(DeleteProjectRequest request) {


        await _mediator.Send(request);

        return Ok();
    }
}
