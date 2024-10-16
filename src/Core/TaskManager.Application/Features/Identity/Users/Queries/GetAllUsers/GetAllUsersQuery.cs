using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<Result<List<User>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetAllUsersQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

}

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<User>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var userList = await _unitOfWork.Repository<User>().GetAllAsync();
        return await Result<List<User>>.SuccessAsync(userList);
    }
}
