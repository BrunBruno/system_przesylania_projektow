using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Enums;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject; 


public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, GetProjectDto> {

    private readonly IUserContextService _userContextService;
    private readonly IProjectRpository _projectRpository;
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;

    public GetProjectRequestHandler(IUserContextService userContextService, IProjectRpository projectRpository, IUserRepository userRepository, IStudentRepository studentRepository) {
        _userContextService = userContextService;
        _projectRpository = projectRpository;
        _userRepository = userRepository;
        _studentRepository = studentRepository;

    }
    public async Task<GetProjectDto> Handle(GetProjectRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var project = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("Nie znaleziono użytkowinka.");

        if (user.RoleId != (int)Roles.Teacher) {
            var student = await _studentRepository.GetStudentByUserIdAndProjectId(userId, project.Id)
                ?? throw new NotFoundException("Nie znaleziono studenta.");

            if (!student.IsAccepted) {
                throw new BadRequestException("Student nie został jeszcze zaakceptowany.");
            }
        } else if(project.Owner.Id  != userId) {
            throw new UnauthorizedAccessException("Nie jesteś właścicielem tego projektu.");
        }

        var projectDto = new GetProjectDto {
            Id = project.Id,
            Name = project.Name,
            OwnerName = project.Owner.LastName,
            Students = project.Students.Select(s => new StudentDto { 
                Id = s.Id,
                Name = s.Name,
                IsAccepted = s.IsAccepted,
                userId = s.UserId,
                Solutions = s.Solutions.Select(sol => new SolutionDto {
                    Id = sol.Id,
                    FileName = sol.FileName,
                    FileType = sol.FileType,
                    TaskId = sol.TaskId,
                }).ToList()
            }).ToList(),
            Tasks = project.Tasks.OrderBy(t => t.TaskNo).Select(t => new TaskDto {
                Id = t.Id,
                TaskNo = t.TaskNo,
                Name = t.Name,
                Description = t.Description,
                EndDate = t.EndDate,
            
            }).ToList()
        };

        return projectDto;
    }
}

