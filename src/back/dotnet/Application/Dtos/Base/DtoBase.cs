namespace Application.Dtos;

public abstract class DtoBase<TDtoID>
{
    public TDtoID Id { get; set; }

    public DtoBase(TDtoID Id)
    {
        this.Id = Id;
    }
}
