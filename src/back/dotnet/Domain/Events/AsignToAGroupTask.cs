namespace Domain.Events;

public sealed class AsignToAGroupTask : IDomainEvent
{
    public DateTime OccurredOn { get; private set; }

    public AsignToAGroupTask()
    {
        OccurredOn = DateTime.Now;
    }
}
