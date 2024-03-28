
namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public int StudentCount { get; set; }
    public int TaskCount { get; set; }
    public List<StudentDto> Students { get; set; }
}

public class StudentDto {
    public Guid UserId { get; set; }
    public bool IsAccepted { get; set; }
}
