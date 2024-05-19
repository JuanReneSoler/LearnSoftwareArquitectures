using System.Linq.Expressions;
using Application.Dtos;

namespace Application.Services;

public interface IBaseService<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    Task<TDto?> Create(TDto Dto, CancellationToken cancellationToken);
    Task<TDtoID> Delete(TDtoID Id, CancellationToken cancellationToken);
    Task<TDto?> Update(TDto Dto, TDtoID Id, CancellationToken cancellationToken);
    Task<IBasePagination<TDto, TDtoID>> Filter(Expression<Func<TDto, bool>> predicate, int page, int size, CancellationToken cancellationToken);
}
