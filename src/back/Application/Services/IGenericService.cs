using Application.Base;

namespace Application.Services;

public interface IGenericService<TDto> : IBaseService<TDto, int>
    where TDto : DtoBase<int>
{
}
