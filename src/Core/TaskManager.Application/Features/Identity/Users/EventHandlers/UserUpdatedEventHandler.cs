using Microsoft.Extensions.Logging;
using TaskManager.Application.Common.Events;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;

namespace TaskManager.Application.Features.Identity.Users.EventHandlers;

public class UserUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<User>>
{
    private readonly ILogger<UserUpdatedEventHandler> _logger;

    public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<User> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}