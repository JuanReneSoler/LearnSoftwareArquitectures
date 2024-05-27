using Domain.Services;
using Infrastructure.Services;

namespace TaskList.Api.Extensions;

public static class InjectServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, GoogleMailService>();
        return services;
    }
}
