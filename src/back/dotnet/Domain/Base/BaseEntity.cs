namespace Domain.Models;

public abstract class BaseEntity<TEntityID>
{
    public TEntityID Id { get; set; }
}
