using TaskList.Api.Middlewares;

namespace TaskList.Api.Extensions;

public static class InjectMiddlewaresExtension
{
    public static IApplicationBuilder InjectMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
        return app;
    }
}
