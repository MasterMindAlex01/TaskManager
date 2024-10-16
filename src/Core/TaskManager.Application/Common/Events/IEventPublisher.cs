using TaskManager.Application.Common.Interfaces;
using TaskManager.Shared.Events;

namespace TaskManager.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}