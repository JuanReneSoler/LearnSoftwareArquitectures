namespace Domain.Models;

public class Person : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Task> Tasks { get; set; }
}
