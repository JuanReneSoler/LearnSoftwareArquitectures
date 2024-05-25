using Domain.Events;

namespace Domain.Entities;

public abstract class BaseEntity<TEntityID> : IGenericAgregate, ISoftDelete<TEntityID>
{
    public TEntityID Id { get; set; }

    protected IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public BaseEntity(TEntityID Id)
    {
        this.Id = Id;
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}
