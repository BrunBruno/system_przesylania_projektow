using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.AcceptStudent;
public class AcceptStudentRequestHandler : IRequestHandler<AcceptStudentRequest> {

    private readonly IStudentRepository _studentRepository;

    public AcceptStudentRequestHandler(IStudentRepository studentRepository) { 
        _studentRepository = studentRepository;
    }

    public async Task Handle(AcceptStudentRequest request, CancellationToken cancellationToken) {

        var student = await _studentRepository.GetStudentById(request.StudentId)
            ?? throw new NotFoundException("Nie znaleziono studenta.");

        if (student.IsAccepted){
            throw new BadRequestException("Student jest już zaakceptowany.");
        }

        student.IsAccepted = true;

        await _studentRepository.UpdateStudent(student);
    }
}
