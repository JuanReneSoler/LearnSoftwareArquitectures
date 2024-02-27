using Application.Dtos;
using Domain;
using System.Linq.Expressions;

namespace Application;

public interface ITaskService : IGenericService<TaskDto, int>
{
    void ReasignToGroup(int TaskId, int GroupId);
}

public class TaskService : ITaskService
{
    private readonly IGenericRepository<Work, int> _repository;

    public TaskService(IGenericRepository<Work, int> Repository)
    {
        _repository = Repository;
    }

    public TaskDto? Add(TaskDto Entity)
    {
        var entity = new Work
        {
            Title = Entity.Title,
            Description = Entity.Description,
            GroupId = Entity.GroupId,
            PersonId = Entity.PersonId
        };
        _repository.Add(entity);
        if (_repository.Commit())
        {
            Entity.Id = entity.Id;
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(TaskDto);
        }
    }

    public int Delete(int Id)
    {
        var entity = _repository.Get(Id);

        if (entity is null) throw new NullReferenceException("Esta Tarea no existe.");

        _repository.Delete(Id);
        return _repository.Commit() ? Id : 0;
    }

    public TaskDto? Update(TaskDto Entity, int Id)
    {
        var entity = _repository.Get(Id);

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");


        entity.Description = Entity.Description;
        entity.Title = Entity.Title;
        entity.GroupId = Entity.GroupId;
        entity.PersonId = Entity.PersonId;
        _repository.Update(entity);
        if (_repository.Commit())
        {
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(TaskDto);
        }
    }

    public IList<TaskDto> GetAll(int? skip, int? take) => _repository.GetAll(skip, take).Select(x => new TaskDto
    {
        Id = x.Id,
        Description = x.Description,
        Title = x.Title,
        GroupId = x.GroupId,
        PersonId = x.PersonId
    }).ToList();

    public IList<TaskDto> Filter(Expression<Func<TaskDto, bool>> predicate, int? skip, int? take)
    {
        var result = _repository.GetAll(skip, take).Select(x => new TaskDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            GroupId = x.GroupId,
            PersonId = x.PersonId
        });
        return result.Where(predicate).ToList();
    }

    public void ReasignToGroup(int TaskId, int GroupId)
    {
        var _work = _repository.Get(TaskId);
        _work.GroupId = GroupId;
        _repository.Commit();
    }
}
