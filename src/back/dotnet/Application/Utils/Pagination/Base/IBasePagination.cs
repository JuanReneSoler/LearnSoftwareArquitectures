namespace Application.UsesCases;

public interface IBasePagination<T>
where T : class
{
    public ICollection<T> Items { get; }
    public int TotalPages { get; }
    public int CurrentPage { get; }
}
