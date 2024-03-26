using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Repositories; 

public interface ITaskRepository {
    Task CreateTask(ProjectTask task);
    Task<ProjectTask?> GetTaskById(Guid taskId);
    Task DeleteTask(ProjectTask task);
}
