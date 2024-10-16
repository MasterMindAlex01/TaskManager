using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Queries;

public class GetUserByIdQuery : IRequest<Result<User?>>
{
    public GetUserByIdQuery(Guid id) => Id = id;

    public Guid Id { get; set; }

}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<User?>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User?>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user =  await _unitOfWork.Repository<User>().GetByIdAsync(query.Id);
        return await Result<User?>.SuccessAsync(user);
    }
}
