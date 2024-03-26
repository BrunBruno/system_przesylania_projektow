using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.TaskRequests.DeleteTask; 
public class DeleteTaskRequestHandler : IRequestHandler<DeleteTaskRequest> {
    private readonly IUserContextService _userContextService;
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskRequestHandler(IUserContextService userContextService, ITaskRepository taskRepository) {
        _taskRepository = taskRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(DeleteTaskRequest request, CancellationToken cancellationToken) {

        var userId = _userContextService.GetUserId()!.Value;

        var taskToDelete = await _taskRepository.GetTaskById(request.TaskId)
            ?? throw new NotFoundException("Nie znaleziono projektu.");

        if (taskToDelete.Project.Owner.Id != userId) {
            throw new BadRequestException("Nie jesteś właścicielem tego projektu.");
        }

        await _taskRepository.DeleteTask(taskToDelete);
    }
}
