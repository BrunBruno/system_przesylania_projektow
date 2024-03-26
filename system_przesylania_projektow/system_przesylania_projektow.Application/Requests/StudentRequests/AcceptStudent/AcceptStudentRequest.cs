using MediatR;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.AcceptStudent; 
public class AcceptStudentRequest :  IRequest {
    public Guid StudentId { get; set; }
}
