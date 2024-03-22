using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.JoinStudent; 

public class JoinStudentRequestHandler : IRequestHandler<JoinStudentRequest> {
    private readonly IUserContextService _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRpository _projectRpository;
    private readonly IStudentRepository _studentRepository;

    public JoinStudentRequestHandler(IUserContextService userContext,
        IUserRepository userRepository,
        IProjectRpository projectRpository,
        IStudentRepository studentRepository) {

        _userContext = userContext;
        _userRepository = userRepository;
        _projectRpository = projectRpository;
        _studentRepository = studentRepository;
    }
    public async Task Handle(JoinStudentRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("Nie znaleziono użytkowika.");

        var project = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        var isStudentInList = project.Students.Any(s => s.UserId == userId);

        if (isStudentInList) {
            throw new BadRequestException("Student jest już zapisany do proejktu.");
        }

        var student = new ProjectStudent {
            Id = Guid.NewGuid(),
            Name = user.FirstName + " " + user.LastName,
            ProjectId = project.Id,
            UserId = userId
        };

        await _studentRepository.CreateStudent(student);
    }
}
