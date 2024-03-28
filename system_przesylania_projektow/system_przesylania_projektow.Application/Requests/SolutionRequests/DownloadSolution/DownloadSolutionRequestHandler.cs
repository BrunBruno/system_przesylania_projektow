

using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadSolution;
public class DownloadSolutionRequestHandler : IRequestHandler<DownloadSolutionRequest, DownloadSolutionDto> {
    private readonly ISolutionRepository _solutionRepository;

    public DownloadSolutionRequestHandler(ISolutionRepository solutionRepository) {
        _solutionRepository = solutionRepository;
    }

    public async Task<DownloadSolutionDto> Handle(DownloadSolutionRequest request, CancellationToken cancellationToken) {
        var solution = await _solutionRepository.GetSolutionById(request.SolutionId)
            ?? throw new NotFoundException("Nie znaleziono rozwiązania.");

        var solutionDto = new DownloadSolutionDto()
        {
            FileContent = solution.DocByte,
            FileName = solution.FileName,
            ContentType = solution.FileType

        };

        return solutionDto;

    }
}
