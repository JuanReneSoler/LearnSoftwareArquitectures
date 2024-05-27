namespace Domain.Entities;

public sealed class Person : BaseEntity<int>
{
    public String Name { get; set; }
    public string Email { get; set; }

    public ICollection<Group>? Groups { get; set; }
    public ICollection<Tasks>? Tasks { get; set; }

    public Person() : base(0)
    {
        Name = string.Empty;
        Email = string.Empty;
        Groups = default;
        Tasks = default;
    }
}
