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
            Console.WriteLine(ex.Message);
            Console.ReadKey();
            await context.Response.WriteAsync("Ocurrio un error interno en el servidor.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
