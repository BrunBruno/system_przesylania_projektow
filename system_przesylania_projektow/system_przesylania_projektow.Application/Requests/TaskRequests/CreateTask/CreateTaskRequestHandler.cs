using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.TaskRequests.CreateTask;
public class CreateTaskRequestHandler : IRequestHandler<CreateTaskRequest> {
    private readonly ITaskRepository _taskRpository;
    private readonly IProjectRpository _projectRpository;

    public CreateTaskRequestHandler(ITaskRepository taskRpository, IProjectRpository projectRpository) {
        _taskRpository = taskRpository;
        _projectRpository = projectRpository;
    }

    public async Task Handle(CreateTaskRequest request, CancellationToken cancellationToken) {

        var project = await _projectRpository.GetProjectById(request.ProjectId)
            ?? throw new NotFoundException("Nie znalezniono projektu.");

        if (project.Tasks.Any(t => t.Name == request.Name)) {
            throw new BadRequestException("Zadanie już istnieje.");
        }

        var task = new ProjectTask() {
            Id = Guid.NewGuid(),
            TaskNo = project.Tasks.Count() + 1,
            Name = request.Name,
            Description = request.Description,
            EndDate = request.EndDate,
            ProjectId = request.ProjectId
        };

        await _taskRpository.CreateTask(task);
    }
}
