namespace Domain.Events;

public sealed class CreateTaskEvent : IDomainEvent
{
    public DateTime OccurredOn { get; private set; }

    public CreateTaskEvent()
    {
        OccurredOn = DateTime.Now;
    }
}