﻿
namespace system_przesylania_projektow.Application.Requests.ProjectRequests.GetProject;
public class GetProjectDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public List<StudentDto> Students { get; set; }
    public List<TaskDto> Tasks { get; set; }
}

public class StudentDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAccepted { get; set; }
    public Guid userId { get; set; }
    public List<SolutionDto> Solutions { get; set; }
}

public class TaskDto {
    public Guid Id { get; set; }
    public int TaskNo { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
}

public class SolutionDto { 
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public Guid TaskId { get; set; }
}
