using Domain.Entities;
using System.Linq.Expressions;
using Application.Dtos;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface IGroupService : IGenericService<GroupDto>
{
}

public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group> _repository;
    private readonly IMapper _mapper;

    public GroupService(
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
        var item = (await _repository.Where(x => x.Id == Id, null, null, cancellationToken)).FirstOrDefault();

        if (item is null) throw new NullReferenceException("Este Grupo no existe.");

        await _repository.Delete(Id, cancellationToken);
        return await _repository.Commit(cancellationToken) ? Id : 0;
    }

    public async Task<GroupDto?> Update(GroupDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, null, null, cancellationToken)).FirstOrDefault();

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

    public async Task<IList<GroupDto>> Filter(Expression<Func<GroupDto, bool>> predicate, int? skip, int? take, CancellationToken cancellationToken)
    {
        var result = (await _repository.Where(x => x.Id > 0, skip, take, cancellationToken)).Select(x => new GroupDto
        {
            Id = x.Id,
            Name = x.Name
        });

        return result.Where(predicate).ToList();
    }
}
