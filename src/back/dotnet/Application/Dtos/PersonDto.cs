using Application.Base;

namespace Application.Dtos;

public class PersonDto : DtoBase<int>
{
    public string Name { get; set; }
}
