using MediatR;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.CreateProject;

public class CreateProjectRequest : IRequest {
    public string ProjectName { get; set; }
}
