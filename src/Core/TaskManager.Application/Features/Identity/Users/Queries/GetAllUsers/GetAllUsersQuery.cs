using MediatR;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Queries;

public class GetAllUsersQuery : IRequest<Result<List<UserResponse>>>
{
}

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var userList = await _userRepository.GetAllUserAsync();
        return await Result<List<UserResponse>>.SuccessAsync(userList);
    }
}
