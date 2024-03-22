using MediatR;
using Microsoft.AspNetCore.Identity;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;
using System.Text.RegularExpressions;
using system_przesylania_projektow.Core.Enums;

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
            throw new BadRequestException($"Użytkownik: {request.Email} już istnieje.");
        }

        if (!request.Password.Equals(request.ConfirmPassword)) {
            throw new BadRequestException("Hasła nie są zgodne.");
        }

        var emailRegex = new Regex("(?<user>[^@]+)@(?<host>.+)");
        var emailMatch = emailRegex.Match(request.Email);

        if (!emailMatch.Success) {
            throw new BadRequestException("Niepoprawny email.");
        }

        var user = new User {
            Id = Guid.NewGuid(),
            Email = request.Email.ToLower(),
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        if (emailMatch.Groups["host"].Value.Contains("student")) {
            user.RoleId = (int)Roles.Student;
        } else {
            user.RoleId = (int)Roles.Teacher;
        }

        await _userRepository.AddUser(user);
    }
}
