using Domain.Events;
using Domain.Services;

namespace Infrastructure.DomainEvents;

public class AsignToGroupNotificationEvent : IDomainEventHandler<AsignToAGroupTask>
{
    private readonly IMailService _service;
    public AsignToGroupNotificationEvent(IMailService service)
    {
        _service = service;
    }

    public void Handle(AsignToAGroupTask domainEvent)
    {
        throw new NotImplementedException();
    }
}
