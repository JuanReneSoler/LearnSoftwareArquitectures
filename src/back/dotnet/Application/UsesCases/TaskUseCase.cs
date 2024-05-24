using Domain.Entities;
using Domain.UnitOfWork;
using Domain.UsesCases;
using EasyMapper;
using System.Linq.Expressions;

namespace Application.UsesCases;

public interface ITaskUseCase : IGenericUseCase<TaskDto>
{
    Task<IBasePagination<TaskDto>> Filter(int GroupId, int PersonId, string Search, int page, int size, CancellationToken cancellationToken);
    Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken);
}

public sealed class TaskUseCase : ITaskUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IDomainEventDispatcher? _eventDispatcher;

    public TaskUseCase(
            IGenericUnitOfWork UoW,
            IMapper Mapper,
            IDomainEventDispatcher eventDispatcher)
    {
        _uow = UoW;
        _mapper = Mapper;
        _eventDispatcher = eventDispatcher;
    }

    public TaskUseCase(
            IGenericUnitOfWork UoW,
            IMapper Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async Task<TaskDto> Create(TaskDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskDto, Tasks>(Dto);
        await _uow.Tasks.Add(entity, cancellationToken);
        if (await _uow.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
        }
        return Dto;
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Tasks.Exist(x => x.Id == Id, cancellationToken))
        {
            await _uow.Tasks.Delete(Id, cancellationToken);
            return await _uow.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar este registro.");
        }
        throw new NullReferenceException("Esta Tarea no existe.");
    }

    public async Task<TaskDto?> Update(TaskDto Dto, int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Tasks.Exist(x => x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<TaskDto, Tasks>(Dto);
            await _uow.Tasks.Update(entity, cancellationToken);
            if (await _uow.Commit(cancellationToken))
            {
                return Dto;
            }
            else
            {
                await _uow.Rollback(cancellationToken);
                return Dto;
            }
        }
        throw new NullReferenceException("Esta Persona no existe.");
    }

    public async Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken)
    {
        if (await _uow.Tasks.Exist(x => x.Id == TaskId, cancellationToken))
        {
            var result = await _uow.Tasks.Where(x => x.Id == TaskId, cancellationToken);
            var task = result.First();

            task.GroupId = GroupId;

            await _uow.Tasks.Update(task, cancellationToken);

            if (await _uow.Commit(cancellationToken))
            {
                if (_eventDispatcher is not null) _eventDispatcher.Dispatch(task.DomainEvents);
                task.ClearEvents();
                return _mapper.Map<Tasks, TaskDto>(task);
            }
            throw new Exception("No fue posible cambiar esta tarea de grupo.");
        }
        throw new Exception("This task not exist!");

    }

    public async Task<TaskDto> Find(int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Tasks.Exist(x => x.Id == Id, cancellationToken))
        {
            var result = await _uow.Tasks.Where(x => x.Id == Id, cancellationToken);
            var entity = result.ToArray()[0];
            return _mapper.Map<Tasks, TaskDto>(entity);
        }
        throw new Exception("Este registro no existe.");
    }

    public async Task<IBasePagination<TaskDto>> Filter(int GroupId, int PersonId, string Search, int page, int size, CancellationToken cancellationToken)
    {
        Expression<Func<Tasks, bool>> expression = (x) => x.Id > 0;

        if (GroupId > 0) expression = expression.And(x => x.GroupId == GroupId);

        if (PersonId > 0) expression = expression.And(x => x.PersonId == PersonId);

        if (!string.IsNullOrEmpty(Search) && !string.IsNullOrWhiteSpace(Search))
        {
            Expression<Func<Tasks, bool>> expressionByTitle = (x) => x.Title.Contains(Search);
            Expression<Func<Tasks, bool>> expressionByDescription = (x) => x.Description.Contains(Search);
            var combineOrExpression = expressionByTitle.Or(expressionByDescription);
            expression = expression.And(combineOrExpression);
        }

        var query = await _uow.Tasks.Where(expression, cancellationToken);
        return query.Select(x => _mapper.Map<Tasks, TaskDto>(x)).Paginate(page, size);
    }
}
