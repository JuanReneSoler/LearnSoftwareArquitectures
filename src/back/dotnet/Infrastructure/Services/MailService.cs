using Domain.Services;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class MailService : IMailService
{
    //
    public async Task Send(CancellationToken cancellationToken)
    {
        SmtpClient smtpClient = new SmtpClient("mi@mail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("mi@mail.com", "contraseña"),
            EnableSsl = true,
        };
        MailMessage mail = new MailMessage
        {
            From = new MailAddress("mi@mail.com"),
            Subject = "asunto",
            Body = "",
            IsBodyHtml = true,
        };
        mail.To.Add("");

        try
        {
            await smtpClient.SendMailAsync(mail, cancellationToken);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
