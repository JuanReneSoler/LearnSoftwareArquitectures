namespace Domain.Services;

public interface IMailService
{
    Task Send(string destinatarioMail, string subject, string message, CancellationToken cancellationToken);
}
