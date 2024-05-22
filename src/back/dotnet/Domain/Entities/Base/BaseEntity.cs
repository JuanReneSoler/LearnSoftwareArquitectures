using Domain.Agregates;
using Domain.Events;

namespace Domain.Entities;

public abstract class BaseEntity<TEntityID> : IGenericAgregate, ISoftDelete<TEntityID>
{
    public TEntityID Id { get; set; }

    private IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public TEntityID CreatedById { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public TEntityID ModifiedById { get; set; }
    public DateTime ModifiedOn { get; set; }

    public BaseEntity(TEntityID Id)
    {
        this.Id = Id;
        //CreatedById
        CreatedOn = default;
        IsDeleted=false;
        IsReadOnly = false;
        //ModifiedById
        ModifiedOn = default;
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}
