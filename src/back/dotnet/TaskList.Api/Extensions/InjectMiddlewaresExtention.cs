using TaskList.Api.Middlewares;

namespace TaskList.Api.Extensions;

public static class InjectMiddlewaresExtension
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
        return app;
    }
}
