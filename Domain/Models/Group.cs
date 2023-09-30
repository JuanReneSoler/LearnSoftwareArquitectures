namespace Domain.Models;

public class Group : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
