using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DomainEventsDispatchers;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(IEnumerable<IDomainEvent> Events, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in Events)
        {
            var eventType = domainEvent.GetType();
            var handlers = _serviceProvider.GetServices(typeof(IDomainEventHandler<>).MakeGenericType(eventType));
            foreach (var handler in handlers)
            {
                if (handler is not null)
                {
                    await ((dynamic)handler).Handle((dynamic)domainEvent, cancellationToken);
                }
            }
        }
    }
}
