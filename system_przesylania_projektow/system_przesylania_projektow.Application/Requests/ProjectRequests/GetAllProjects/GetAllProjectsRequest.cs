using MediatR;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequest : IRequest<List<GetAllProjectsDto>> {
    public string? Name { get; set; }

}
