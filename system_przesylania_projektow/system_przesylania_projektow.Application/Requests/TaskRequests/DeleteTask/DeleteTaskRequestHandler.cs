using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.TaskRequests.DeleteTask; 
public class DeleteTaskRequestHandler : IRequestHandler<DeleteTaskRequest> {
    private readonly IUserContextService _userContextService;
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRpository _projectRpository;

    public DeleteTaskRequestHandler(IUserContextService userContextService, ITaskRepository taskRepository, IProjectRpository projectRpository) {
        _taskRepository = taskRepository;
        _userContextService = userContextService;
        _projectRpository = projectRpository;
    }

    public async Task Handle(DeleteTaskRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var taskToDelete = await _taskRepository.GetTaskById(request.TaskId)
            ?? throw new NotFoundException("Nie znaleziono zadania.");

        if (taskToDelete.Project.Owner.Id != userId) {
            throw new BadRequestException("Nie jesteś właścicielem tego projektu.");
        }

        var project = await _projectRpository.GetProjectById(taskToDelete.ProjectId) 
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        await _taskRepository.DeleteTask(taskToDelete);

        var tasksToUpdate = project.Tasks
            .Where(task => task.TaskNo > taskToDelete.TaskNo)
            .OrderBy(task => task.TaskNo);

        int newTaskNo = taskToDelete.TaskNo;

        foreach (var task in tasksToUpdate)
        {
            task.TaskNo = newTaskNo++;
            await _taskRepository.UpdateTask(task);
        }
    }
}
