using Domain.Entities;
using System.Linq.Expressions;
using Application.Dtos;
using Application.Base;
using Domain.Base;

namespace Application;

public interface IGroupService : IGenericService<GroupDto, int>
{
    //
}

public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group, int> _repository;

    public GroupService(IGenericRepository<Group, int> Repository)
    {
        _repository = Repository;
    }
    public GroupDto? Add(GroupDto Entity)
    {
        var group = new Group
        {
            Name = Entity.Name,
        };
        _repository.Add(group);
        if (_repository.Commit())
        {
            Entity.Id = group.Id;
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(GroupDto);
        }
    }


    public int Delete(int Id)
    {
        var item = _repository.Get(Id);

        if (item is null) throw new NullReferenceException("Este Grupo no existe.");

        _repository.Delete(Id);
        return _repository.Commit() ? Id : 0;
    }
    public GroupDto? Update(GroupDto Entity, int Id)
    {
        var entity = _repository.Get(Id);

        if (entity is null) throw new NullReferenceException("Este Grupo no existe.");

        entity.Name = Entity.Name;
        _repository.Update(entity);
        if (_repository.Commit())
        {
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(GroupDto);
        }
    }

    public IList<GroupDto> GetAll(int? skip, int? take)
       => _repository.GetAll(skip, take).Select(x => new GroupDto
       {
           Id = x.Id,
           Name = x.Name
       }).ToList();

    public IList<GroupDto> Filter(Expression<Func<GroupDto, bool>> predicate, int? skip, int? take)
    {
        var result = _repository.GetAll(skip, take).Select(x => new GroupDto
        {
            Id = x.Id,
            Name = x.Name
        });

        return result.Where(predicate).ToList();
    }
}
