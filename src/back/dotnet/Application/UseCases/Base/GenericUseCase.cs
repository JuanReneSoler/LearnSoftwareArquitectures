using Application.Dtos;
using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.UnitsOfWork;

public abstract class GenericUseCase<TEntity, TEntityDto> : IGenericUseCase<TEntityDto>
    where TEntity : BaseEntity<int>
    where TEntityDto : DtoBase<int>
{
    private readonly IMapperService _mapper;
    private readonly IGenericUnitOfWork _uow;
    public GenericUseCase(IGenericUnitOfWork UoW, IMapperService Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async virtual Task<TEntityDto> Create(TEntityDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntityDto, TEntity>(Dto);
        await _uow.GetRepository<TEntity>().Add(entity, cancellationToken);
        if (await _uow.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        throw new Exception("No fue posible persistir el registro.");
    }

    public async virtual Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        await _uow.GetRepository<TEntity>().Delete(Id, cancellationToken);
        return await _uow.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar el registro.");
    }

    public async virtual Task<TEntityDto> Find(int Id, CancellationToken cancellationToken)
    {
        var result = await _uow.GetRepository<TEntity>().Find(Id, cancellationToken);
        return _mapper.Map<TEntity, TEntityDto>(result);
    }

    public async virtual Task<TEntityDto?> Update(TEntityDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntityDto, TEntity>(Dto);
        await _uow.GetRepository<TEntity>().Update(entity, cancellationToken);
        if (await _uow.Commit(cancellationToken))
        {
            return Dto;
        }
        throw new Exception("No fue posible actualizar el registro.");
    }
}
