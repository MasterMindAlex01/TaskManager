using Microsoft.Extensions.Logging;
using TaskManager.Application.Common.Events;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;

namespace TaskManager.Application.Features.Identity.Users.EventHandlers;

public class UserCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<User>>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<User> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}