namespace Domain.Entities;

public abstract class BaseEntity<TEntityID>
{
    public TEntityID Id { get; set; }

    public BaseEntity(TEntityID Id)
    {
        this.Id = Id;
    }
}
