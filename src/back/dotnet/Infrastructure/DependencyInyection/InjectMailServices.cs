using Domain.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInyection;

public static class InjectMailServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
        return services;
    }
}
