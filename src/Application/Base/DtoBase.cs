namespace Application.Base;

public abstract class DtoBase<TEntityID>
{
    public TEntityID Id { get; set; }
}
