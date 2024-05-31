using Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _config;
    public MailService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task Send(string destinatarioMail, string subject, string message, CancellationToken cancellationToken)
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

        smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        MailMessage mail = new MailMessage
        {
            From = new MailAddress(correo),
            Subject = subject,
            Body = message,
            IsBodyHtml = false,
        };
        mail.To.Add(destinatarioMail);

        await smtpClient.SendMailAsync(mail, cancellationToken);
    }

    private void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Console.WriteLine($"Error al enviar el correo: {e.Error.ToString()}");
        }
        else if (e.Cancelled)
        {
            Console.WriteLine("Envio de correo cancelado.");
        }
        else Console.WriteLine("envio de correo exitoso!");
    }
}
