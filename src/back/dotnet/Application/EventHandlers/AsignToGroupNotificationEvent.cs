using Domain.Entities;
using Domain.Events;
using Domain.Services;
using Domain.UnitsOfWork;

namespace Application.EventHandlers;

public class AsignToGroupNotificationEvent : IDomainEventHandler<AsignToAGroupTask>
{
    private readonly IMailService _service;
    private readonly IGenericUnitOfWork _uow;

    public AsignToGroupNotificationEvent(
            IMailService service,
            IGenericUnitOfWork unitOfWork)
    {
        _service = service;
        _uow = unitOfWork;
    }

    public async Task Handle(AsignToAGroupTask domainEvent, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _uow.GetRepository<Person>().Where(x => x.Id == domainEvent.Task.PersonId, cancellationToken);
            var array = result.ToArray();
            if (array.Any())
            {
                var entity = array[0];
                var message = $"Estimado {entity.Name}, la tarea '{domainEvent.Task.Title}' le ha sido asignada.\n\n\n Este mensaje fue enviado a tu correo {entity.Email} automaticamte. Por favor no responder!";
                await _service.Send(entity.Email, "Asignacion de tarea", message, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
