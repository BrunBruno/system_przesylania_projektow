
using MediatR;
using Microsoft.AspNetCore.Identity;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Core.Entities;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.UserRequests.SignIn;

public class SignInRequestHandler : IRequestHandler<SignInRequest, SignInDto> {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;

    public SignInRequestHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService) {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<SignInDto> Handle(SignInRequest request, CancellationToken cancellationToken) {
        var user = await _userRepository.GetUserByEmail(request.Email.ToLower());

        if (user is null){
            throw new BadRequestException("Invalid email or password.");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed) {
            throw new BadRequestException("Invalid email or password");
        }

        var token = _jwtService.GetJwtToken(user);

        return new SignInDto() {
            Token = token
        };
    }
}
