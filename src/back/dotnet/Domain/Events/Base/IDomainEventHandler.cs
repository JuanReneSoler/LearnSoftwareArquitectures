namespace Domain.Events;

public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    void Handle(TEvent domainEvent);
}
