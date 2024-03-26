using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Infrastructure.EF.Contexts;

namespace system_przesylania_projektow.Infrastructure.EF.Repositories;
internal class SolutionRepository : ISolutionRepository {
    private readonly AppDbContext _dbContext;

    public SolutionRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task AddSolution(ProjectSolution solution) {
        await _dbContext.Solutions.AddAsync(solution);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProjectSolution?> GetSolutionById(Guid solutionId)
     => await _dbContext.Solutions
         .FirstOrDefaultAsync(x => x.Id == solutionId);

    public async Task DeleteSolution(ProjectSolution solution) {
        _dbContext.Solutions.Remove(solution);
        await _dbContext.SaveChangesAsync();
    }
}
