namespace Domain.Base;

public abstract class BaseEntity<TEntityID>
{
    public TEntityID Id { get; set; }
}
