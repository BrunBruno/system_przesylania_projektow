using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadAllSoludtions;

public class DownloadAllSolutionsRequestHandler : IRequestHandler<DownloadAllSolutionsRequest, DownloadAllSolutionsDto> {

    private readonly IProjectRpository _projectRpository;

    public DownloadAllSolutionsRequestHandler(IProjectRpository projectRpository) {
        _projectRpository = projectRpository;
    }

    public async Task<DownloadAllSolutionsDto> Handle(DownloadAllSolutionsRequest request, CancellationToken cancellationToken) {

        var project = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        var solutionsDto = new DownloadAllSolutionsDto {
            ProjectName = project.Name,
            OwnerName = project.Owner.LastName + "_" + project.Owner.FirstName,

            Students = project.Students.Where(stu => stu.IsAccepted).Select(stu => new StudentDto { 
                StudentName = stu.Name,

                Solutions = stu.Solutions.Select(sol => new SolutionDto {
                    TaskName = sol.TaskName,
                    FileContent = sol.DocByte,
                    FileName = sol.FileName,
                    ContentType = sol.FileType
                }).ToList(),
            }).ToList(),
        };

        return solutionsDto;
    }
}
