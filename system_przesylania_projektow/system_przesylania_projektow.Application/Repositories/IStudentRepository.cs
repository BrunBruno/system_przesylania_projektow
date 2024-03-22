using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application.Repositories; 
public interface IStudentRepository {
    Task CreateStudent(ProjectStudent student);
    Task<ProjectStudent?> GetStudentById(Guid id);
    Task UpdateStudent(ProjectStudent student);
    Task<ProjectStudent?> GetStudentByUserIdAndProjectId(Guid userId, Guid projectId);
}
