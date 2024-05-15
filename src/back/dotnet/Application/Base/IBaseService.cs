using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    Task<TDto?> Create(TDto Dto, CancellationToken cancellationToken);
    Task<TDtoID> Delete(TDtoID Id, CancellationToken cancellationToken);
    Task<TDto?> Update(TDto Dto, TDtoID Id, CancellationToken cancellationToken);
    Task<IList<TDto>> Filter(Expression<Func<TDto, bool>> predicate, int? skip, int? take, CancellationToken cancellationToken);
}
