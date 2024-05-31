namespace TaskList.Api.Middlewares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //await context.Response.WriteAsync(ex.Message);
            Console.WriteLine(ex.Message);
        }
    }
}
