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
        Console.WriteLine("prueba de un evento de dominio ejecutandose XD");
        throw new NotImplementedException();
    }
}