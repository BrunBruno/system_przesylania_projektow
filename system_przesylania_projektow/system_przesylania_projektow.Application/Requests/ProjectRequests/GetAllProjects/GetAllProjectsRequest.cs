using MediatR;
using system_przesylania_projektow.Application.Pagination;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequest : IRequest<PagedResult<GetAllProjectsDto>> {
    public int ElementsCount { get; set; } = 10;
}
