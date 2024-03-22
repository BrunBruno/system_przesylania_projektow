using MediatR;

namespace system_przesylania_projektow.Application.Requests.UserRequests.RegisterUser;

public class RegisterUserRequest : IRequest {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
