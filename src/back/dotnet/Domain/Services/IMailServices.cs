namespace Domain.Services;

public interface IMailService
{
    Task Send(CancellationToken cancellationToken);
}
