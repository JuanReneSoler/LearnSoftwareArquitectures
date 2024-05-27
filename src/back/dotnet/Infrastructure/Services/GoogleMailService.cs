using Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class GoogleMailService : IMailService
{
    private readonly IConfiguration _config;
    public GoogleMailService(IConfiguration configuration)
    {
        _config = configuration;
    }
    public async Task Send(string destinatarioMail, string message, CancellationToken cancellationToken)
    {
        var smtp = _config["MailConfiguration:Smtp"] ?? string.Empty;
        var correo = _config["MailConfiguration:Correo"] ?? string.Empty;
        var password = _config["MailConfiguration:Password"] ?? string.Empty;

        SmtpClient smtpClient = new SmtpClient(smtp)
        {
            Port = 587,
            Credentials = new NetworkCredential(correo, password),
            EnableSsl = true,
        };
        MailMessage mail = new MailMessage
        {
            From = new MailAddress(correo),
            Subject = "Asignacion de tarea",
            Body = message,
            IsBodyHtml = false,
        };
        mail.To.Add(destinatarioMail);

        await smtpClient.SendMailAsync(mail, cancellationToken);
        Console.WriteLine("main send successfull");
    }
}
