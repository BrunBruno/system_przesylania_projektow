
namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadAllSoludtions; 

public class DownloadAllSolutionsDto {
    public string ProjectName {  get; set; }
    public string OwnerName { get; set; }

    public List<StudentDto> Students { get; set; }
}

public class StudentDto {
    public string StudentName { get; set; }
    public List<SolutionDto> Solutions { get; set; }
}

public class SolutionDto {
    public string TaskName { get; set; }
    public byte[] FileContent { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
