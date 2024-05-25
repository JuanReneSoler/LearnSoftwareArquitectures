using Microsoft.AspNetCore.Mvc;

namespace TaskList.Api.Utils;

public class Result<T>
{
    public T? Value { get; private set; }
    public IActionResult ActionResult { get; private set; }
    public string Message { get; set; }

    private Result(T? Value, IActionResult ActionResult, string Message)
    {
        this.Value = Value;
        this.ActionResult = ActionResult;
        this.Message = Message;
    }

    private static Result<T> Success(T Value) => new Result<T>(Value, new OkResult(), "Success");
    private static Result<T> Failure(IActionResult Result, string Message) => new Result<T>(default(T), Result, Message);
}
