
using MediatR;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Shared.Exeptions;

namespace system_przesylania_projektow.Application.Requests.UserRequests.GetUser;

public class GetUserRequestHandler : IRequestHandler<GetUserRequest, GetUserDto>
{
    private readonly IUserContextService _userContext;
    private readonly IUserRepository _userRepository;

    public GetUserRequestHandler(IUserContextService userContext, IUserRepository userRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
    }
    public async Task<GetUserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId()!.Value;

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("User not found");

        var userDto = new GetUserDto
        {
            Email = user.Email
        };

        return userDto;
    }
}