using System.Linq.Expressions;
using Application.Extensions;
using Application.Utils;
using Domain.Entities;
using Domain.Repositories;
using EasyMapper;

namespace Application.UsesCases;

public interface IGroupUseCase : IGenericUseCase<GroupDto>
{
}

public sealed class GroupsUseCase : IGroupUseCase
{
    private readonly IGenericRepository<Group> _repository;
    private readonly IMapper _mapper;

    public GroupsUseCase(
            IGenericRepository<Group> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
    }

    public async Task<GroupDto> Create(GroupDto Dto, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<GroupDto, Group>(Dto);
        await _repository.Add(group, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = group.Id;
            return Dto;
        }
        else{
            await _repository.Rollback(cancellationToken);
            throw new Exception("No fue posible crear el registro.");
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            await _repository.Delete(Id, cancellationToken);
            return await _repository.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar ele registro.");
        }
        throw new NullReferenceException("Este Grupo no existe.");
    }

    public async Task<GroupDto?> Update(GroupDto Dto, int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=> x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<GroupDto, Group>(Dto);
            await _repository.Update(entity, cancellationToken);
            if (await _repository.Commit(cancellationToken))
            {
                return Dto;
            }
            else
            {
                await _repository.Rollback(cancellationToken);
                throw new Exception("No fue posible actualizar el registro.");
            }
        }
        throw new Exception("El registro que esta intentando actualizar no existe.");
    }

    public async Task<IBasePagination<GroupDto>> Filter(Expression<Func<GroupDto, bool>> predicate, int page, int size, CancellationToken cancellationToken)
    {
        var query = await _repository.Select(x => new GroupDto
        {
            Id = x.Id,
            Name = x.Name
        }, predicate, cancellationToken);

        return query?.Paginate(page, size);
    }

    public async Task<GroupDto> Find(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            var select = await _repository.Select(x=>new GroupDto{
                Name=x.Name,
                Id=x.Id
            }, x=>x.Id == Id, cancellationToken);

            return select.First();
        }
        throw new Exception("Este registro no existe.");
    }
}
