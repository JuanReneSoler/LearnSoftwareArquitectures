namespace Domain;

public class Person : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Task> Tasks { get; set; }
}
