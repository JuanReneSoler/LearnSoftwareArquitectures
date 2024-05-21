using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;
using Application.Utils;
using Application.Extensions;

namespace Application.UsesCases;

public interface ITaskUseCase : IGenericUseCase<TaskDto>
{
    Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken);
}

public sealed class TaskUseCase : ITaskUseCase
{
    private readonly IGenericRepository<Tasks> _repository;
    private readonly IMapper _mapper;

    public TaskUseCase(
            IGenericRepository<Tasks> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
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
                return default(TaskDto);
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
        var work = (await _repository.Where(x => x.Id == TaskId, cancellationToken)).FirstOrDefault();

        if (work is null) throw new Exception("This task not exist!");

        work.GroupId = GroupId;

        await _repository.Update(work, cancellationToken);

        await _repository.Commit(cancellationToken);

        return _mapper.Map<Tasks, TaskDto>(work);
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