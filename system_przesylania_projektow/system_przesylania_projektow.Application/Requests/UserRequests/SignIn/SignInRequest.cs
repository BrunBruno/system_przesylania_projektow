using MediatR;

namespace system_przesylania_projektow.Application.Requests.UserRequests.SignIn;

public class SignInRequest : IRequest<SignInDto> {
    public string Email { get; set; }
    public string Password { get; set; }
}
