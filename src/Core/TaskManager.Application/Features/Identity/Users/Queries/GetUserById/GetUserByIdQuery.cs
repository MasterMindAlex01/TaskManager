using MediatR;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Queries;

public class GetUserByIdQuery : IRequest<Result<UserResponse?>>
{
    public GetUserByIdQuery(Guid id) => Id = id;

    public Guid Id { get; set; }

}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponse?>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse?>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user =  await _userRepository.GetUserByIdAsync(query.Id);
        return await Result<UserResponse?>.SuccessAsync(user);
    }
}
