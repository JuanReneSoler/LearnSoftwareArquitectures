using Application.Dtos;
using Application.Extensions;
using Application.Utils;
using Domain.Entities;
using Domain.Services;
using Domain.UnitsOfWork;
using System.Linq.Expressions;

namespace Application.UseCases;

public interface IGroupUseCase : IGenericUseCase<GroupDto>
{
    Task<IBasePagination<GroupDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken);
}

public sealed class GroupsUseCase : GenericUseCase<Group, GroupDto>, IGroupUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapperService _mapper;

    public GroupsUseCase(
            IGenericUnitOfWork UoW,
            IMapperService Mapper) : base(UoW, Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async Task<IBasePagination<GroupDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken)
    {
        Expression<Func<Group, bool>> expression = x => x.Id > 0;

        if (!string.IsNullOrEmpty(Search) && !string.IsNullOrWhiteSpace(Search))
        {
            expression = expression.And(x => x.Name.Contains(Search));
        }

        var query = await _uow.GetRepository<Group>().Where(expression, cancellationToken);
        return query.Select(x => _mapper.Map<Group, GroupDto>(x)).Paginate(page, size);
    }
}
