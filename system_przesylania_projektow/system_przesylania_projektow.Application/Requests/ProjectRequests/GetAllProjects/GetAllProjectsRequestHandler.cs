
using MediatR;
using system_przesylania_projektow.Application.Pagination;
using system_przesylania_projektow.Application.Repositories;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequestHandler : IRequestHandler<GetAllProjectsRequest, PagedResult<GetAllProjectsDto>> {

    private readonly IProjectRpository _projectRpository;

    public GetAllProjectsRequestHandler(IProjectRpository projectRpository) {
        _projectRpository = projectRpository;
    }

    public async Task<PagedResult<GetAllProjectsDto>> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken) {

        var projects = await _projectRpository.GetAllProjects();

        if (request.Name is not null)
        {
            projects = projects.Where(p =>
                p.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase) ||
                (p.Owner.FirstName + " " + p.Owner.LastName).Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }


        var projectDtos = projects.Select(project => new GetAllProjectsDto {
            Id = project.Id,
            Name = project.Name,
            OwnerName = project.Owner.FirstName + " " + project.Owner.LastName,
            StudentCount = project.Students.Count(),
            TaskCount = project.Tasks.Count(),
        });

       
        var pagedResult = new PagedResult<GetAllProjectsDto>(projectDtos.ToList(), projectDtos.Count(), request.ElementsCount, 1);

        return pagedResult;
    }
}
