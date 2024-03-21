
using MediatR;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject;
public class GetProjectRequest : IRequest<GetProjectDto> {
    public Guid Id { get; set; }
}