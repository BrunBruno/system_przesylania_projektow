using MediatR;

namespace system_przesylania_projektow.Application.Requests.TaskRequests.DeleteTask; 
public class DeleteTaskRequest : IRequest {
    public Guid TaskId { get; set; }
}
