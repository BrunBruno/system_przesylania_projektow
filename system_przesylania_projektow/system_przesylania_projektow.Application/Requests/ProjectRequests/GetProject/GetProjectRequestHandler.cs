using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject; 


public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, GetProjectDto> {
    private readonly IProjectRpository _projectRpository;

    public GetProjectRequestHandler(IProjectRpository projectRpository) {
        _projectRpository = projectRpository;
    }
    public async Task<GetProjectDto> Handle(GetProjectRequest request, CancellationToken cancellationToken) {

        var project = await _projectRpository.GetProjectById(request.Id)
            ?? throw new NotFoundException("Game not found");


        var projectDto = new GetProjectDto
        {
            Id = project.Id,
            Name = project.Name
        };

        return projectDto;
    }
}

