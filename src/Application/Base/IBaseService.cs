using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TDto, TEntityID>
    where TDto : DtoBase<TEntityID>
{
    TDto? Create(TDto Entity);
    TEntityID Delete(TEntityID Id);
    TDto? Update(TDto Entity, TEntityID Id);
    IList<TDto> Filter(Expression<Func<TDto, bool>> predicate, int? skip, int? take);
}
