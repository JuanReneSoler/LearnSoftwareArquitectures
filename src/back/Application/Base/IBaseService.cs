using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    TDto? Create(TDto Dto);
    TDtoID Delete(TDtoID Id);
    TDto? Update(TDto Dto, TDtoID Id);
    IList<TDto> Filter(Expression<Func<TDto, bool>> predicate, int? skip, int? take);
}
