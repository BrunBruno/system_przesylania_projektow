using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Infrastructure.EF.Contexts;

namespace system_przesylania_projektow.Infrastructure.EF.Repositories;

public  class ProjectRpository : IProjectRpository {

    private readonly AppDbContext _dbContext;

    public ProjectRpository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateProject(Project game) {
        await _dbContext.Projects.AddAsync(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Project?> GetProjectById(Guid id)
        => await _dbContext.Projects
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Project>> GetAllProjects()
        => await _dbContext.Projects
            .ToListAsync();

    public async Task UpdateProject(Project order) {
        _dbContext.Projects.Update(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProject(Project order) {
        _dbContext.Projects.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}
