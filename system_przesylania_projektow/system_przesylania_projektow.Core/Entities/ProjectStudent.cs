

namespace system_przesylania_projektow.Core.Entities; 
public class ProjectStudent {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAccepted { get; set; } = false;

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
