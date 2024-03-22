
using MediatR;

namespace system_przesylania_projektow.Application.Requests.TaskRequests.CreateTask;

public class CreateTaskRequest : IRequest {
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
}
