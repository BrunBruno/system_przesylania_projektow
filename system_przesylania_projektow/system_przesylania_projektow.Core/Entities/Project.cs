
namespace system_przesylania_projektow.Core.Entities;

public class Project {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid OwnerId { get; set; }
    public User Owner { get; set; }

    public List<ProjectStudent> Students { get; set; }
    public List<ProjectTask> Tasks { get; set; }
}
