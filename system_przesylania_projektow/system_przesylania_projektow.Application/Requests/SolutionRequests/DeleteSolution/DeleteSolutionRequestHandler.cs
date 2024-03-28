using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DeleteSolution; 


public class DeleteSolutionRequestHandler : IRequestHandler<DeleteSolutionRequest> {
    private readonly ISolutionRepository _solutionRepository;
    private readonly IUserContextService _userContextService;

    public DeleteSolutionRequestHandler(ISolutionRepository solutionRepository, IUserContextService userContextService) {
        _solutionRepository = solutionRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(DeleteSolutionRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var solutionToDelete = await _solutionRepository.GetSolutionById(request.SolutionId)
            ?? throw new NotFoundException("Nie znaleziono rozwiązania.");

        await _solutionRepository.DeleteSolution(solutionToDelete);
    }
}
