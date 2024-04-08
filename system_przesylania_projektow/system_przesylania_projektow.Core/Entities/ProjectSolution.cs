
namespace system_przesylania_projektow.Core.Entities;

public class ProjectSolution {
    public Guid Id { get; set; }

    public string FileName { get; set; }
    public string FileType { get; set; }
    public byte[] DocByte { get; set; }

    public Guid StudentId { get; set; }
    public ProjectStudent Student { get; set; }

    public Guid TaskId { get; set; }
    public ProjectTask Task { get; set; }
    public string TaskName { get; set; }


}
