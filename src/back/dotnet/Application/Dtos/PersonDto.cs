namespace Application.Dtos;

public class PersonDto : DtoBase<int>
{
    public string Name { get; set; }
    public string Email { get; set; }

    public PersonDto() : base(0)
    {
        this.Name = string.Empty;
        this.Email = string.Empty;
    }
}
