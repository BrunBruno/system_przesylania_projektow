using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.RejectStudent;

public class RejectStudentRequestHandler : IRequestHandler<RejectStudentRequest> {

    private readonly IStudentRepository _studentRepository;

    public RejectStudentRequestHandler(IStudentRepository studentRepository) {
        _studentRepository = studentRepository;
    }

    public async Task Handle(RejectStudentRequest request, CancellationToken cancellationToken) {

        var student = await _studentRepository.GetStudentById(request.StudentId) 
            ?? throw new NotFoundException("Nie znaleziono studenta");

        await _studentRepository.DeleteStudent(student);
    }
}
