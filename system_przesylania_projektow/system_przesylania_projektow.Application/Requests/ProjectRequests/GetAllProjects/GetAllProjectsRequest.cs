using MediatR;
using system_przesylania_projektow.Application.Pagination;

namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsRequest : IRequest<PagedResult<GetAllProjectsDto>> {
    public string? Name { get; set; }
    public int PageNumber { get; set; }
    public int ElementsCount { get; set; } = 10;
}
