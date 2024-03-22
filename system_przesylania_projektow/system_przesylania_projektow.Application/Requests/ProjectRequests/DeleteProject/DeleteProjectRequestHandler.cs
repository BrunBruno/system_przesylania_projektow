using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.DeleteProject; 

public class DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest> {
    private readonly IProjectRpository _projectRpository;
    private readonly IUserContextService _userContextService;

    public DeleteProjectRequestHandler(IProjectRpository projectRpository, IUserContextService userContextService) {
        _projectRpository = projectRpository;
        _userContextService = userContextService;
    }

    public async Task Handle(DeleteProjectRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var projectToDelete = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        if (projectToDelete.OwnerId != userId) {
            throw new BadRequestException("Nie jesteś właścicielem tego projektu.");
        }

        await _projectRpository.DeleteProject(projectToDelete);
    }
}
