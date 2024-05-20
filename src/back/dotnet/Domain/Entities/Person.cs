namespace Domain.Models;

public sealed class Person : BaseEntity<int>
{
    public string Name { get; set; }

    public ICollection<Group> Groups { get; set; }
    public ICollection<Tasks> Tasks { get; set; }

    public Person() : base(0)
    {
        this.Name = string.Empty;
        this.Groups = new List<Group>();
        this.Tasks = new List<Tasks>();
    }
}
