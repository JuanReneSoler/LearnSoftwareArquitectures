namespace Application.UsesCases;

public class GroupDto : DtoBase<int>
{
    public string Name { get; set; }

    public GroupDto() : base(0)
    {
        this.Name = string.Empty;
    }
}
