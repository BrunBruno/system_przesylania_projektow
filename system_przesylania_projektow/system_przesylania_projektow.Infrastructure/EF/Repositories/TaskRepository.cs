
using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Infrastructure.EF.Contexts;

namespace system_przesylania_projektow.Infrastructure.EF.Repositories;
public class TaskRepository : ITaskRepository {

    private readonly AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateTask(ProjectTask task) {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProjectTask?> GetTaskById(Guid taskId) 
        => await _dbContext.Tasks
            .Include(x => x.Project)
            .ThenInclude(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == taskId);

    public async Task DeleteTask(ProjectTask task) {
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTask(ProjectTask task) {
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();
    }
}
