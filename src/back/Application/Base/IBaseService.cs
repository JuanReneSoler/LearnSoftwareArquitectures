using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    Task<TDto?> Create(TDto Dto);
    Task<TDtoID> Delete(TDtoID Id);
    Task<TDto?> Update(TDto Dto, TDtoID Id);
    Task<IList<TDto>> Filter(Expression<Func<TDto, bool>> predicate, int? skip, int? take);
}
