using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    TDto? Create(TDto Entity);
    TDtoID Delete(TDtoID Id);
    TDto? Update(TDto Entity, TDtoID Id);
    IList<TDto> Filter(Expression<Func<TDto, bool>> predicate, int? skip, int? take);
}
