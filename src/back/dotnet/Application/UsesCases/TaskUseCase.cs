using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;
using Application.Utils;
using Application.Extensions;
using Application.Dispatchers;

namespace Application.UsesCases;

public interface ITaskUseCase : IGenericUseCase<TaskDto>
{
    Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken);
}

public sealed class TaskUseCase : ITaskUseCase
{
    private readonly IGenericRepository<Tasks> _repository;
    private readonly IMapper _mapper;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public TaskUseCase(
            IGenericRepository<Tasks> Repository,
            IMapper Mapper,
            IDomainEventDispatcher eventDispatcher)
    {
        _repository = Repository;
        _mapper = Mapper;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<TaskDto> Create(TaskDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskDto, Tasks>(Dto);
        await _repository.Add(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
        }
        return Dto;
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            await _repository.Delete(Id, cancellationToken);
            return await _repository.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar este registro.");
        }
        throw new NullReferenceException("Esta Tarea no existe.");
    }

    public async Task<TaskDto?> Update(TaskDto Dto, int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<TaskDto, Tasks>(Dto);
            await _repository.Update(entity, cancellationToken);
            if (await _repository.Commit(cancellationToken))
            {
                return Dto;
            }
            else
            {
                await _repository.Rollback(cancellationToken);
                return Dto;
            }
        }
        throw new NullReferenceException("Esta Persona no existe.");        
    }

    public async Task<IBasePagination<TaskDto>> Filter(Expression<Func<TaskDto, bool>> predicate, int page, int size, CancellationToken cancellationToken)
    {
        var result = await _repository.Select(x => new TaskDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            GroupId = x.GroupId,
            Group = new GroupDto
            {
                Id = x.GroupId,
                Name = x.Group.Name,
            },
            PersonId = x.PersonId,
            Person = new PersonDto
            {
                Id = x.PersonId,
                Name = x.Person.Name
            }
        }, predicate, cancellationToken);
        return result?.Paginate(page, size);
    }

    public async Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == TaskId, cancellationToken))
        {
            var result = await _repository.Where(x=>x.Id == TaskId, cancellationToken);
            var task = result.First();

            task.GroupId = GroupId;

            await _repository.Update(task, cancellationToken);

            if(await _repository.Commit(cancellationToken))
            {
                _eventDispatcher.Dispatch(task.DomainEvents);
                task.ClearEvents();
                return _mapper.Map<Tasks, TaskDto>(task);
            }
            throw new Exception("No fue posible cambiar esta tarea de grupo.");
        }
        throw new Exception("This task not exist!");
        
    }

    public async Task<TaskDto> Find(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            var result = await _repository.Select(x => new TaskDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                GroupId = x.GroupId,
                Group = new GroupDto
                {
                    Id = x.GroupId,
                    Name = x.Group.Name,
                },
                PersonId = x.PersonId,
                Person = new PersonDto
                {
                    Id = x.PersonId,
                    Name = x.Person.Name
                }
            }, x=>x.Id == Id, cancellationToken);
            return result.First();
        }
        throw new Exception("Este registro no existe.");
    }
}