namespace Application.Dtos;

public class PersonDto : DtoBase<int>
{
    public string Name { get; set; }

    public PersonDto() : base(0)
    {
        this.Name = string.Empty;
    }
}
