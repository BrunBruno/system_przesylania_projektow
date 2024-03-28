
using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Enums;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequestHandler : IRequestHandler<GetAllProjectsRequest, List<GetAllProjectsDto>> {

    private readonly IProjectRpository _projectRpository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetAllProjectsRequestHandler(IUserContextService userContextService, IProjectRpository projectRpository, IUserRepository userRepository) {
        _userContextService = userContextService;
        _projectRpository = projectRpository;
        _userRepository = userRepository;
    }

    public async Task<List<GetAllProjectsDto>> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var projects = await _projectRpository.GetAllProjects();

        var user = await _userRepository.GetUserById(userId)
         ?? throw new NotFoundException("Nie znaleziono użytkowinka.");

        if (user.RoleId == (int)Roles.Teacher) {
            projects = projects.Where(p => p.OwnerId == userId);
        }

        if (request.Name is not null) {
            projects = projects.Where(p =>
                p.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase) ||
                (p.Owner.FirstName + " " + p.Owner.LastName).Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        var projectDtos = projects.OrderByDescending(p => p.CreationDate).Select(project => new GetAllProjectsDto {
            Id = project.Id,
            Name = project.Name,
            OwnerName = project.Owner.FirstName + " " + project.Owner.LastName,
            StudentCount = project.Students.Count(),
            TaskCount = project.Tasks.Count(),

            Students = project.Students.Select(s => new StudentDto { 
                UserId = s.UserId,
                IsAccepted = s.IsAccepted
            }).ToList()
        }).ToList();

        return projectDtos;
    }
}
