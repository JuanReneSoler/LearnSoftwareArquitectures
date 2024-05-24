using Domain.Events;

namespace Domain.UsesCases;

public interface IDomainEventDispatcher
{
    void Dispatch(IEnumerable<IDomainEvent> Events);
}
