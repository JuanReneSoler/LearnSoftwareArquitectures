using Domain.Base;

namespace Domain;

public class Person : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Work> Tasks { get; set; }
}
