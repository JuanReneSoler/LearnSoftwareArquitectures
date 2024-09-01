using Application.EventHandlers;
using Domain.Events;
using Infrastructure.DomainEventsDispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInyection;

public static class InjectDomainEvent
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventHandler<AsignToAGroupTask>, AsignToGroupNotificationEvent>();
        return services;
    }
}
