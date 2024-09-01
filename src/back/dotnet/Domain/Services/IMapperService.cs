namespace Domain.Services;

public interface IMapperService
{
    TOutput Map<TInput, TOutput>(TInput input);
}
