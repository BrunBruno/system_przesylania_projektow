

namespace system_przesylania_projektow.Core.Entities; 
public class TaskSolution {
    public Guid Id { get; set; }
    public bool isFinished { get; set; } = false;
    
    

    public Guid StudentId { get; set; }
    public ProjectStudent ProjectStudent { get; set; }

    public Guid TaskId { get; set; }
    public ProjectTask ProjectTask { get; set; }
}
