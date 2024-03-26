using MediatR;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.UpdateProjec; 

public class UpdateProjectRequest : IRequest {
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}
