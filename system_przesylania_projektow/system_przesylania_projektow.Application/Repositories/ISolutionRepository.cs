using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Repositories; 

public interface ISolutionRepository {
    Task AddSolution(ProjectSolution solution);
    Task<ProjectSolution?> GetSolutionById(Guid solutionId);
    Task DeleteSolution(ProjectSolution solution);
}
