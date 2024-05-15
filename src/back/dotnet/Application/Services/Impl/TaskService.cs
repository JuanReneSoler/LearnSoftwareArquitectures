using Application.Dtos;
using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface ITaskService : IGenericService<TaskDto>
{
    Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken);
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

    public async Task<TaskDto?> Create(TaskDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskDto, Tasks>(Dto);
        await _repository.Add(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(TaskDto);
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, null, null, cancellationToken)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Tarea no existe.");

        await _repository.Delete(Id, cancellationToken);
        return await _repository.Commit(cancellationToken) ? Id : 0;
    }

    public async Task<TaskDto?> Update(TaskDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, null, null, cancellationToken)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");


        entity.Description = Dto.Description;
        entity.Title = Dto.Title;
        entity.GroupId = Dto.GroupId;
        entity.PersonId = Dto.PersonId;
        await _repository.Update(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(TaskDto);
        }
    }

    public async Task<IList<TaskDto>> Filter(Expression<Func<TaskDto, bool>> predicate, int? skip, int? take, CancellationToken cancellationToken)
    {
        var result = await _repository.Select(x=> new TaskDto{
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            GroupId = x.GroupId,
            Group = new GroupDto{
                Id = x.GroupId,
                Name = x.Group.Name,
            },
            PersonId = x.PersonId,
            Person = new PersonDto{
                Id = x.PersonId,
                Name = x.Person.Name
            }
        }, predicate, skip, take, cancellationToken);
        return result.ToList();
    }

    public async Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken)
    {
        var work = (await _repository.Where(x => x.Id == TaskId, null, null, cancellationToken)).FirstOrDefault();

        if (work is null) throw new Exception("This task not exist!");

        work.GroupId = GroupId;

        await _repository.Update(work, cancellationToken);

        await _repository.Commit(cancellationToken);

        return _mapper.Map<Tasks, TaskDto>(work);
    }
}
