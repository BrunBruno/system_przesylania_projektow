

using MediatR;
using System.Numerics;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.UpdateProjec;
public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest> {
    private readonly IUserContextService _userContext;
    private readonly IProjectRpository _projectRepository;


    public UpdateProjectRequestHandler(IUserContextService userContext, IProjectRpository projectRepository) {
        _userContext = userContext;
        _projectRepository = projectRepository;
    }

    public async Task Handle(UpdateProjectRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;

        var project = await _projectRepository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Project was not found");

        project.Name = request.Name;


        await _projectRepository.UpdateProject(project);
    }
}
