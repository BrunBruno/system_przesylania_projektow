
using MediatR;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DeleteSolution; 
public class DeleteSolutionRequest : IRequest {
    public Guid SolutionId { get; set; }
}
