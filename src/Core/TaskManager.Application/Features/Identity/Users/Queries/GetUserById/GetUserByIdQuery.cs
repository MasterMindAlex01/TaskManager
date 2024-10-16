using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<Result>
{
    public GetUserByIdQuery(Guid id) => Id = id;

    public Guid Id { get; set; }

}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await _userReadService.GetUserByIdAsync(query.Id);
    }
}
