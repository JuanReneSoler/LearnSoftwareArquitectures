namespace Domain;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
