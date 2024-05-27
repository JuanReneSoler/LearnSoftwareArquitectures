namespace Domain.Services;

public interface IMailService
{
    Task Send(string destinatarioMail, string message, CancellationToken cancellationToken);
}
