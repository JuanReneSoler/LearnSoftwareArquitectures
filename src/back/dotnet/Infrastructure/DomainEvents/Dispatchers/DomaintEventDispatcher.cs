using Application.Dispatchers;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DomainEvents;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    
    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch(IEnumerable<IDomainEvent> Events)
    {
        foreach(var domainEvent in Events)
        {
            var eventType = domainEvent.GetType();
            var handlers = _serviceProvider.GetServices(typeof(IDomainEventHandler<>).MakeGenericType(eventType));
            foreach(var handler in handlers)
            {
                ((dynamic)handler).Handle((dynamic)domainEvent);
            }
        }
    }
}