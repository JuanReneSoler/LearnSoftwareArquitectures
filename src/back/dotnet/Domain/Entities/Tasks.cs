using Domain.Events;

namespace Domain.Entities;

public sealed class Tasks : BaseEntity<int>
{
    public String Title { get; set; }
    public String Description { get; set; }

    private Int32 _groupId;
    public Int32 GroupId
    {
        get => _groupId;
        set
        {
            if (value != _groupId)
            {
                var changeGroupEvent = new AsignToAGroupTask();
                _domainEvents.Add(changeGroupEvent);
                _groupId = value;
            }
        }
    }
    public Int32 PersonId { get; set; }

    public Person? Person { get; set; }
    public Group? Group { get; set; }

    public Tasks() : base(0)
    {
        Title = string.Empty;
        Description = string.Empty;
        GroupId = 0;
        PersonId = 0;
        Person = default;
        Group = default;
    }
}
