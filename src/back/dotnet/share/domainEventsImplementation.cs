using Microsoft.Extensions.DependencyInjection;

public interface IDomainEvent
{
    DateTime OccurredOn{get;}
}

public class OrderPlacedEvent : IDomainEvent
{
    //
    public DateTime OccurredOn {get; private set;}
    public int OrderId {get; private set;}

    public OrderPlacedEvent(int orderId)
    {
        OrderId=orderId;
        OccurredOn=DateTime.UtcNow;
    }
}

public class Order
{
    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public int Id { get; private set; } = 0;
    public string Status { get; private set;} = string.Empty;

    public void PlaceOrder()
    {
        Status="Placed";
        var orderPlacedEvent = new OrderPlacedEvent(Id);
        _domainEvents.Add(orderPlacedEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public interface IDomainEventDispatcher
{
    void Dispatch(IEnumerable<IDomainEvent> events);
}

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch(IEnumerable<IDomainEvent> events)
    {
        foreach(var domainEvent in events)
        {
            var eventType = domainEvent.GetType();
            var handlers = _serviceProvider.GetServices(typeof(IDomainEventHandler<>).MakeGenericType(eventType));
            foreach(var handler in handlers)
            {
                ((dynamic)handler).Handle((dynamic)domainEvent);
            }
        }
    }
}

public interface IDomainEventHandler<TEvent> where TEvent:IDomainEvent
{
    void Handle(TEvent domainEvent);
}

public class OrderPlacedEventHandler : IDomainEventHandler<OrderPlacedEvent>
{
    public void Handle(OrderPlacedEvent domainEvent)
    {
        Console.WriteLine("order placed");
    }
}

public class OrderService
{
    private readonly IDomainEventDispatcher _eventDispatcher;

    public OrderService(IDomainEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public void PlaceOrder(Order order)
    {
        order.PlaceOrder();

        _eventDispatcher.Dispatch(order.DomainEvents);
        order.ClearDomainEvents();
    }
}

public class Program
{
    public static void Main()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        serviceCollection.AddScoped<IDomainEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
        IServiceProvider provider = serviceCollection.BuildServiceProvider();
        using(var scope = provider.CreateScope())
        {
            var scopeServiceProvider = scope.ServiceProvider;
            var dispatcher = scopeServiceProvider.GetService<IDomainEventDispatcher>();
            if(dispatcher is not null)
            {
                var service = new OrderService(dispatcher);
                service.PlaceOrder(new Order());
            }
        }
    }
}




















