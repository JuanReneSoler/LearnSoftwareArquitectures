using Application.Dtos;
using Application.Utils;

namespace Application.Extensions;

public static class Paginator
{
    public static GenericPagination<TDto> Paginate<TDto>(this IQueryable<TDto> query, int page, int size)
    where TDto : DtoBase<int>
    {
        return new GenericPagination<TDto>(query, page, size);
    }
}
