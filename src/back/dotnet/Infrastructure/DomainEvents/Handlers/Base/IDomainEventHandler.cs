using Domain.Events;

namespace Infrastructure.DomainEvents;

public interface IDomainEventHandler<TEvent>
    where TEvent:IDomainEvent
{
    void Handle(TEvent domainEvent);
}