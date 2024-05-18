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
        catch(OperationCanceledException)
        {
            await context.Response.WriteAsync("La solicitud fue cancelada.");
            context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
        }
        catch (NullReferenceException)
        {
            await context.Response.WriteAsync("No se encontraron elementos con el parametro seleccionado.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Ocurrio un error interno en el servidor.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
