using MediatR;
using Microsoft.AspNetCore.Http;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.AddSolution; 
public class AddSolutionRequest : IRequest {
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }
    public IFormFile File { get; set; }
}
