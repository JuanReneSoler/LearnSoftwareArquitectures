namespace Application.UsesCases;

public interface IGenericUseCase<TDto> : IBaseUseCase<TDto, int>
    where TDto : DtoBase<int>
{
}
