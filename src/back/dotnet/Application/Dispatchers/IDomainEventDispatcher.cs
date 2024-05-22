using Domain.Events;

namespace Application.Dispatchers;

public interface IDomainEventDispatcher
{
    void Dispatch(IEnumerable<IDomainEvent> Events);
}