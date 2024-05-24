namespace Application.UsesCases;

public sealed class GenericPagination<TDto> : IBasePagination<TDto>
    where TDto : class
{
    private readonly IQueryable<TDto> _query;
    private readonly int _size;
    private readonly int _page;

    public ICollection<TDto> Items { get => _query.Skip((_page - 1) * _size).Take(_size).ToArray(); }
    public int TotalPages { get => ((_query.Count() + _size - 1) / _size); }
    public int CurrentPage { get => _page; }

    internal GenericPagination(IQueryable<TDto> Query, int Page, int Size)
    {
        this._query = Query;
        this._page = Page;
        this._size = Size;
    }
}
