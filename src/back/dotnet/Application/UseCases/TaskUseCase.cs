using Application.Dtos;
using Application.Extensions;
using Application.Utils;
using Domain.Entities;
using Domain.Services;
using Domain.UnitsOfWork;
using System.Linq.Expressions;

namespace Application.UseCases;

public interface ITaskUseCase : IGenericUseCase<TaskDto>
{
    Task<IBasePagination<TaskDto>> Filter(int GroupId, int PersonId, string Search, int page, int size, CancellationToken cancellationToken);
    Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken);
}

public sealed class TaskUseCase : GenericUseCase<Tasks, TaskDto>, ITaskUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapperService _mapper;

    public TaskUseCase(
            IGenericUnitOfWork UoW,
            IMapperService Mapper) : base(UoW, Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
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

        var query = await _uow.GetRepository<Tasks>().Where(expression, cancellationToken);
        return query.Select(x => _mapper.Map<Tasks, TaskDto>(x)).Paginate(page, size);
    }

    public async Task<TaskDto> ReasignToGroup(int TaskId, int GroupId, CancellationToken cancellationToken)
    {
        if (await _uow.GetRepository<Tasks>().Exist(x => x.Id == TaskId, cancellationToken))
        {
            var result = await _uow.GetRepository<Tasks>().Where(x => x.Id == TaskId, cancellationToken);
            var task = result.First();

            task.GroupId = GroupId;

            await _uow.GetRepository<Tasks>().Update(task, cancellationToken);

            if (await _uow.Commit(cancellationToken))
            {
                return _mapper.Map<Tasks, TaskDto>(task);
            }
            throw new Exception("No fue posible cambiar esta tarea de grupo.");
        }
        throw new Exception("This task not exist!");

    }
}
