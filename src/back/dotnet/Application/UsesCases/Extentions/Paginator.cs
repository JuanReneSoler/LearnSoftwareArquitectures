namespace Application.UsesCases;

internal static class Paginator
{
    internal static GenericPagination<TDto> Paginate<TDto>(this IQueryable<TDto> query, int page, int size)
    where TDto : DtoBase<int>
    {
        return new GenericPagination<TDto>(query, page, size);
    }
}
