
namespace TaskList.Api.Dtos;

public class LoginModel
{
    public string User { get; set; }
    public string Password { get; set; }

    public LoginModel()
    {
        this.User = string.Empty;
        this.Password = string.Empty;
    }
}
