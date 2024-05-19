using Application.Dtos;

namespace Application.Services;

public interface IBasePagination<TDto, TDtoID>
where TDto : DtoBase<TDtoID>
{
    public IList<TDto>? Items { get; }
    public int TotalPages { get; }
    public int CurrentPage { get; }
}
