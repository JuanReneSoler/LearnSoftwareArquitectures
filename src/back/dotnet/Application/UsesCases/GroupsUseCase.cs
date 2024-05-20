using System.Linq.Expressions;
using Application.Dtos;
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

    public async Task<GroupDto?> Create(GroupDto Dto, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<GroupDto, Group>(Dto);
        await _repository.Add(group, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = group.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(GroupDto);
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        var item = (await _repository.Where(x => x.Id == Id, cancellationToken)).FirstOrDefault();

        if (item is null) throw new NullReferenceException("Este Grupo no existe.");

        await _repository.Delete(Id, cancellationToken);
        return await _repository.Commit(cancellationToken) ? Id : 0;
    }

    public async Task<GroupDto?> Update(GroupDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, cancellationToken)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Este Grupo no existe.");

        entity.Name = Dto.Name;
        await _repository.Update(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(GroupDto);
        }
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
}
