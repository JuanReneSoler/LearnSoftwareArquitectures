using Application.Dtos;

namespace Application.UseCases;

public interface IBaseUseCase<TDto, TDtoID>
    where TDto : DtoBase<TDtoID>
{
    Task<TDto> Find(TDtoID Id, CancellationToken cancellationToken);
    Task<TDto> Create(TDto Dto, CancellationToken cancellationToken);
    Task<TDtoID> Delete(TDtoID Id, CancellationToken cancellationToken);
    Task<TDto?> Update(TDto Dto, TDtoID Id, CancellationToken cancellationToken);
}
