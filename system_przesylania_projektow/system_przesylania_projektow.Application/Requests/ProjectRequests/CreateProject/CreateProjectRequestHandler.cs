using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.CreateProject;

public class CreateProjectRequestHandler : IRequestHandler<CreateProjectRequest> {

    private readonly IUserContextService _userContext;
    private readonly IProjectRpository _projectRpository;

    public CreateProjectRequestHandler(IUserContextService userContext, IProjectRpository projectRpository) {
        _userContext = userContext;
        _projectRpository = projectRpository;
    }

    public async Task Handle(CreateProjectRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;

        var project = new Project() {
            Id = Guid.NewGuid(),
            Name = request.ProjectName,
            OwnerId = userId
        };

        await _projectRpository.CreateProject(project);
    }
}