namespace Domain.Events;

public interface IDomainEventDispatcher
{
    void Dispatch(IEnumerable<IDomainEvent> Events);
}
