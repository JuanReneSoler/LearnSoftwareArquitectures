using Application.Dtos;

namespace Application.Services;

public sealed class GenericPagination<TDto> : IBasePagination<TDto, int>
    where TDto : DtoBase<int>
{
    private readonly IQueryable<TDto> _query;
    private readonly int _size;
    private readonly int _page;

    public IList<TDto>? Items { get => _query.Skip((_page - 1) * _size).Take(_size).ToList(); }
    public int TotalPages { get => (_query.Count() / _size) + 1; }
    public int CurrentPage { get => _page; }

    public GenericPagination(IQueryable<TDto> Query, int Page, int Size)
    {
        this._query = Query;
        this._page = Page;
        this._size = Size;
    }
}
