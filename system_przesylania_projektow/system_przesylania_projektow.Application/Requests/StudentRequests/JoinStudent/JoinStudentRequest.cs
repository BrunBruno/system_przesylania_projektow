
using MediatR;

namespace system_przesylania_projektow.Application.Requests.StudentRequests.JoinStudent; 

public class JoinStudentRequest : IRequest {
    public Guid ProjectId { get; set; }
}
