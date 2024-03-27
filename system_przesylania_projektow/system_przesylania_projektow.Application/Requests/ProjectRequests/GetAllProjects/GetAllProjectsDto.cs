
namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetAllProjects; 

public class GetAllProjectsDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public int StudentCount { get; set; }
    public int TaskCount { get; set; }
}
