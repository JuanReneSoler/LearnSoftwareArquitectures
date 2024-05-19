namespace Domain.Models;

public sealed class Person : BaseEntity<int>
{
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Tasks> Tasks { get; set; }
}
