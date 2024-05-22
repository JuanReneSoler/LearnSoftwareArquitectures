using Domain.Events;

namespace Domain.Agregates;

public interface IGenericAgregate
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearEvents();
}