using Domain.Events;
using Domain.UsesCases;
using Infrastructure.DomainEvents;

namespace TaskList.Api.Extensions;

public static class InjectDomainEventExtentions
{
    public static IServiceCollection InjectDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventHandler<AsignToAGroupTask>, AsignToGroupNotificationEvent>();
        return services;
    }
}
