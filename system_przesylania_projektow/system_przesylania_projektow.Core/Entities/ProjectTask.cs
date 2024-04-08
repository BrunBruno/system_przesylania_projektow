

namespace system_przesylania_projektow.Core.Entities; 
public class ProjectTask {
    public Guid Id { get; set; }
    public int TaskNo { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public DateTime EndDate { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public List<ProjectSolution> Solutions { get; set; }

}
