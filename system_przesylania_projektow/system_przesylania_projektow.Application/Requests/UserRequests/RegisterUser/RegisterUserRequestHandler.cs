using MediatR;
using Microsoft.AspNetCore.Identity;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.UserRequests.RegisterUser;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserRequestHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher){
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(RegisterUserRequest request, CancellationToken cancellationToken) {

        var emailAlreadyExists = await _userRepository.GetUserByEmail(request.Email.ToLower());

        if (emailAlreadyExists is not null) {
            throw new BadRequestException($"User with email: {request.Email} already exists.");
        }

        if (!request.Password.Equals(request.ConfirmPassword)) {
            throw new BadRequestException("Passwords don't match.");
        }

        var user = new User {
            Id = Guid.NewGuid(),
            Email = request.Email.ToLower(),
        };

        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        await _userRepository.AddUser(user);
    }
}
