namespace Domain.Entities;

public sealed class Tasks : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public int PersonId { get; set; }

    public Person? Person { get; set; }
    public Group? Group { get; set; }

    public Tasks() : base(0)
    {
        this.Title = string.Empty;
        this.Description = string.Empty;
        this.GroupId = 0;
        this.PersonId = 0;
        this.Person = default;
        this.Group = default;
    }
}
