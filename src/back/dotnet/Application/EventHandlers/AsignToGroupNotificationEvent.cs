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
            var result = await _uow.People.Where(x => x.Id == domainEvent.Task.PersonId, cancellationToken);
            var array = result.ToArray();
            if (array.Any())
            {
                var entity = array[0];
                var message = $"la tarea {domainEvent.Task.Title} the ha sido asignada {entity.Name}, este mensaje fue enviado a tu correo {entity.Email} automaticamte.";
                await _service.Send(entity.Email, message, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }
}
