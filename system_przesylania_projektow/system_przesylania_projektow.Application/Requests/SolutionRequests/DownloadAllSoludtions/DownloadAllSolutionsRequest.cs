using MediatR;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadAllSoludtions; 

public class DownloadAllSolutionsRequest : IRequest<DownloadAllSolutionsDto> {
    public Guid ProjectId { get; set; }
}
