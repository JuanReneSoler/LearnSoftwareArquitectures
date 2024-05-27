namespace Domain.Events;

public interface IDomainEventDispatcher
{
    Task Dispatch(IEnumerable<IDomainEvent> Events, CancellationToken cancellationToken);
}
