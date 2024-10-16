using Microsoft.Extensions.Logging;
using TaskManager.Application.Common.Events;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;

namespace TaskManager.Application.Features.Identity.Users.EventHandlers;
public class UserDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<User>>
{
    private readonly ILogger<UserDeletedEventHandler> _logger;

    public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<User> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}