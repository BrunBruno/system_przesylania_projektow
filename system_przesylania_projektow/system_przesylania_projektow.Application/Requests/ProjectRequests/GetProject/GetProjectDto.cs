
namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject;
public class GetProjectDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public List<StudentDto> Students { get; set; }
    public List<TaskDto> Tasks { get; set; }
}

public class StudentDto { 
    public string Name { get; set; }
}

public class TaskDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
}
