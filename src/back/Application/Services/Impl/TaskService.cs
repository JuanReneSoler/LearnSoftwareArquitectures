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
        var entity = (await _repository.Where(cancellationToken, x => x.Id == Id, null, null)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Tarea no existe.");

        await _repository.Delete(Id, cancellationToken);
        return await _repository.Commit(cancellationToken) ? Id : 0;
    }

    public async Task<TaskDto?> Update(TaskDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(cancellationToken, x => x.Id == Id, null, null)).FirstOrDefault();

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

    public async Task<IList<TaskDto>> Filter(CancellationToken cancellationToken, Expression<Func<TaskDto, bool>> predicate, int? skip, int? take)
    {
        var result = (await _repository.Where(cancellationToken, x => x.Id > 0, skip, take)).Select(x => new TaskDto
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

    public async Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken)
    {
        var work = (await _repository.Where(cancellationToken, x => x.Id == TaskId, null, null)).FirstOrDefault();

        if (work is null) throw new Exception("This task not exist!");

        work.GroupId = GroupId;

        await _repository.Update(work, cancellationToken);

        await _repository.Commit(cancellationToken);

        return _mapper.Map<Tasks, TaskDto>(work);
    }
}
