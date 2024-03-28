

using MediatR;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadSolution; 
public class DownloadSolutionRequest :IRequest<DownloadSolutionDto> {
    public Guid SolutionId { get; set; }
}
