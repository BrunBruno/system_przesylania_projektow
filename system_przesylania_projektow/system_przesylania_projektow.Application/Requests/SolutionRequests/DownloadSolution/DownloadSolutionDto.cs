

namespace system_przesylania_projektow.Application.Requests.SolutionRequests.DownloadSolution; 
public class DownloadSolutionDto {
    public byte[] FileContent { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
