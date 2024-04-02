using MediatR;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.RejectStudent; 
public class RejectStudentRequest : IRequest {
    public Guid StudentId { get; set; }
}
