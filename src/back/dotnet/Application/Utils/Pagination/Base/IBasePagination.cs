namespace Application.Utils;

public interface IBasePagination<T>
where T : class
{
    public IList<T>? Items { get; }
    public int TotalPages { get; }
    public int CurrentPage { get; }
}
