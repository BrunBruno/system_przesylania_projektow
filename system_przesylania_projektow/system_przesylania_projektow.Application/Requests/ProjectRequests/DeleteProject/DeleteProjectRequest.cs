

using MediatR;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.DeleteProject;
public class DeleteProjectRequest : IRequest {
    public Guid ProjectId { get; set; }
}
