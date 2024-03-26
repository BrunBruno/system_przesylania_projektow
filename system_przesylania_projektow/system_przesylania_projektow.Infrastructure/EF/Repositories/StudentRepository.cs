using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Infrastructure.EF.Contexts;

namespace system_przesylania_projektow.Infrastructure.EF.Repositories;
public class StudentRepository : IStudentRepository {

    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateStudent(ProjectStudent student) {
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProjectStudent?> GetStudentById(Guid id)
        => await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task UpdateStudent(ProjectStudent student) {
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProjectStudent?> GetStudentByUserIdAndProjectId(Guid userId, Guid projectId) 
        => await _dbContext.Students
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
}
