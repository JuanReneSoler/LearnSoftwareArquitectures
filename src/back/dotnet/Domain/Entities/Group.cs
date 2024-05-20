namespace Domain.Models;

public sealed class Group : BaseEntity<int>
{
    public String Name { get; set; }

    public ICollection<Tasks> Tasks { get; set; }

    public Group() : base(0)
    {
        this.Name = string.Empty;
        this.Tasks = new List<Tasks>();
    }
}
