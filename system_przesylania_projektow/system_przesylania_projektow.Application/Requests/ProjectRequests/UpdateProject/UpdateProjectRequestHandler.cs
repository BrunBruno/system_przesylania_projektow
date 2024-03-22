using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.UpdateProjec;

public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest> {
    private readonly IProjectRpository _projectRepository;
    private readonly IUserContextService _userContextService;


    public UpdateProjectRequestHandler(IProjectRpository projectRepository, IUserContextService userContextService) {
        _projectRepository = projectRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(UpdateProjectRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var project = await _projectRepository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        if (project.OwnerId != userId) {
            throw new BadRequestException("Nie jesteś właścicielem tego projektu.");
        }

        project.Name = request.Name;

        await _projectRepository.UpdateProject(project);
    }
}
