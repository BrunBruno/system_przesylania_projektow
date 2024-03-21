
using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.DeleteProject; 

public class DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest> {
    private readonly IUserContextService _userContextService;
    private readonly IProjectRpository _projectRpository;

    public DeleteProjectRequestHandler(IUserContextService userContextService, IProjectRpository projectRpository) {
        _userContextService = userContextService;
        _projectRpository = projectRpository;
    }

    public async Task Handle(DeleteProjectRequest request, CancellationToken cancellationToken) {
        var userId = _userContextService.GetUserId()!.Value;

        var projectToDelete = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Order was not found.");

        await _projectRpository.DeleteProject(projectToDelete);

    }
}
