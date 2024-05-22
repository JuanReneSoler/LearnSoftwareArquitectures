using Domain.Events;
using Domain.Services;

namespace Infrastructure.DomainEvents;

public class ChangeGroupTaskEventHandler : IDomainEventHandler<CreateTaskEvent>
{
    private readonly IMailService _mailService;

    public ChangeGroupTaskEventHandler(IMailService mailService)
    {
        _mailService = mailService;
    }

    public void Handle(CreateTaskEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}