using Domain.Services;
using Infrastructure.Services;

namespace TaskList.Api.Extensions;

public static class InjectServicesExtension
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
        return services;
    }
}
