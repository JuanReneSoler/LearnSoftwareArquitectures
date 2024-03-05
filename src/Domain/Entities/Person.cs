using Domain.Base;

namespace Domain.Entities;

public class Person : BaseEntity<int>
{
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Tasks> Tasks { get; set; }
}
