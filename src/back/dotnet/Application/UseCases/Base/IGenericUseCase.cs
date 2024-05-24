using Application.Dtos;

namespace Application.UseCases;

public interface IGenericUseCase<TDto> : IBaseUseCase<TDto, int>
    where TDto : DtoBase<int>
{
}
