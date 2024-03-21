
using MediatR;
using system_przesylania_projektow.Application.Pagination;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequestHandler : IRequestHandler<GetAllProjectsRequest, PagedResult<GetAllProjectsDto>> {
    private readonly IUserContextService _userContext;
    private readonly IProjectRpository _projectRpository;

    public GetAllProjectsRequestHandler(IUserContextService userContext, IProjectRpository projectRpository) {

        _userContext = userContext;
        _projectRpository = projectRpository;
    }
    public async Task<PagedResult<GetAllProjectsDto>> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;

        var projects = await _projectRpository.GetAllProjects();

        var projectDtos = projects.Select(project => new GetAllProjectsDto {
            Id = project.Id,
            Name = project.Name
        });

        var pagedResult = new PagedResult<GetAllProjectsDto>(projectDtos.ToList(), projectDtos.Count(), request.ElementsCount, 1);

        return pagedResult;
    }
}
