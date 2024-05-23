using Domain.Events;

namespace Domain.Entities;

public interface IGenericAgregate
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearEvents();
}