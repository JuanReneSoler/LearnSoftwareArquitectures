using Application.Base;

namespace Application.Dtos;

public class GroupDto : DtoBase<int>
{
    public string Name { get; set; }
}
