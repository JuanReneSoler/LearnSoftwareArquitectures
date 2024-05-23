using Domain.Entities;

namespace Domain.Events;

public sealed class AsignToAGroupTask : IDomainEvent
{
    public DateTime OccurredOn { get; private set; }
    public Tasks Task { get; set; }

    public AsignToAGroupTask(Tasks task)
    {
        Task = task;
        OccurredOn = DateTime.Now;
    }
}
