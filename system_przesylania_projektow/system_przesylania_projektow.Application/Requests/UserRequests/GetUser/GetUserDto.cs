namespace system_przesylania_projektow.Application.Requests.UserRequests.GetUser;

public class GetUserDto {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }

}
