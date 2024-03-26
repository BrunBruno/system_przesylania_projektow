using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request) {
        await _mediator.Send(request);   
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects(GetAllProjectsRequest request) {
        var projects = await _mediator.Send(request);
        return Ok(projects);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProject(GetProjectRequest request) {
        var project = await _mediator.Send(request);
        return Ok(project);
    }

    [HttpPut("{projectId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UpdateProject(UpdateProjectRequest request) {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{projectId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteProject(DeleteProjectRequest request) {
        await _mediator.Send(request);
        return Ok();
    }
}
