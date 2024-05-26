using Application.EventHandlers;
using Domain.Events;
using Infrastructure.DomainEventsDispatchers;

namespace TaskList.Api.Extensions;

public static class InjectDomainEventExtentions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventHandler<AsignToAGroupTask>, AsignToGroupNotificationEvent>();
        return services;
    }
}
