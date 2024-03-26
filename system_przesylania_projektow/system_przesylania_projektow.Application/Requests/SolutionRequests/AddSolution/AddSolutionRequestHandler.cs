using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.AddSolution; 
public class AddSolutionRequestHandler : IRequestHandler<AddSolutionRequest> {
    private readonly IUserContextService _userContext;
    private readonly ISolutionRepository _solutionRepository;

    public AddSolutionRequestHandler(IUserContextService userConext, ISolutionRepository solutionRepository) {
        _userContext = userConext;
        _solutionRepository = solutionRepository;
    }

    public async Task Handle(AddSolutionRequest request, CancellationToken cancellationToken) {
        var userId = _userContext.GetUserId()!.Value;
    

        var memoryStream = new MemoryStream();
        request.File.CopyTo(memoryStream);


        var solution = new ProjectSolution() {
            Id = Guid.NewGuid(),
            FileName = request.File.FileName,
            FileType = request.File.ContentType,
            DocByte = memoryStream.ToArray(),
            StudentId = userId
        };

        await _solutionRepository.AddSolution(solution);
    }
}
