using Application.Dtos;
using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface ITaskService : IGenericService<TaskDto>
{
    void ReasignToGroup(int TaskId, int GroupId);
}

public class TaskService : ITaskService
{
    private readonly IGenericRepository<Tasks> _repository;
    private readonly IMapper _mapper;

    public TaskService(
            IGenericRepository<Tasks> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
    }

    public TaskDto? Create(TaskDto Dto)
    {
        var entity = _mapper.Map<TaskDto, Tasks>(Dto);
        _repository.Add(entity);
        if (_repository.Commit())
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            _repository.Rollback();
            return default(TaskDto);
        }
    }

    public int Delete(int Id)
    {
        var entity = _repository.Where(x => x.Id == Id, null, null).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Tarea no existe.");

        _repository.Delete(Id);
        return _repository.Commit() ? Id : 0;
    }

    public TaskDto? Update(TaskDto Dto, int Id)
    {
        var entity = _repository.Where(x => x.Id == Id, null, null).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");


        entity.Description = Dto.Description;
        entity.Title = Dto.Title;
        entity.GroupId = Dto.GroupId;
        entity.PersonId = Dto.PersonId;
        _repository.Update(entity);
        if (_repository.Commit())
        {
            return Dto;
        }
        else
        {
            _repository.Rollback();
            return default(TaskDto);
        }
    }

    public IList<TaskDto> Filter(Expression<Func<TaskDto, bool>> predicate, int? skip, int? take)
    {
        var result = _repository.Where(x => x.Id > 0, skip, take).Select(x => new TaskDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            GroupId = x.GroupId,
            PersonId = x.PersonId,
            Person = new PersonDto
            {
                Id = x.Person.Id,
                Name = x.Person.Name
            },
            Group = new GroupDto
            {
                Id = x.Group.Id,
                Name = x.Group.Name
            }
        });
        return result.Where(predicate).ToList();
    }

    public void ReasignToGroup(int TaskId, int GroupId)
    {
        var _work = _repository.Where(x => x.Id == TaskId, null, null).FirstOrDefault();

        if(_work is null) throw new Exception("This task not exist!");

        _work.GroupId = GroupId;

        _repository.Commit();
    }
}
