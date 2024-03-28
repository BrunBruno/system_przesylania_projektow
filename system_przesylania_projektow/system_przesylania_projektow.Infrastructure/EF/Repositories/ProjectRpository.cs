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

    public async Task CreateProject(Project project) {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Project?> GetProjectById(Guid id)
        => await _dbContext.Projects
            .Include(x => x.Owner)
            .Include(x => x.Students)
            .ThenInclude(s => s.Solutions)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Project>> GetAllProjects()
        => await _dbContext.Projects
            .Include(x => x.Owner)
            .Include(x => x.Students)
            .Include(x => x.Tasks)
            .ToListAsync();

    public async Task UpdateProject(Project project) {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProject(Project project) {
        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();
    }
}
