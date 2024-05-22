namespace Domain.Entities;

public sealed class Group : BaseEntity<int>
{
    public String Name { get; set; }

    public ICollection<Tasks>? Tasks { get; set; }

    public Group() : base(0)
    {
        Name = string.Empty;
        Tasks = default;
    }
}
