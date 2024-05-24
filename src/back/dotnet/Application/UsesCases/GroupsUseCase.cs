using Domain.Entities;
using Domain.UnitOfWork;
using EasyMapper;
using System.Linq.Expressions;

namespace Application.UsesCases;

public interface IGroupUseCase : IGenericUseCase<GroupDto>
{
    Task<IBasePagination<GroupDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken);
}

public sealed class GroupsUseCase : IGroupUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GroupsUseCase(
            IGenericUnitOfWork UoW,
            IMapper Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async Task<GroupDto> Create(GroupDto Dto, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<GroupDto, Group>(Dto);
        await _uow.Groups.Add(group, cancellationToken);
        if (await _uow.Commit(cancellationToken))
        {
            Dto.Id = group.Id;
            return Dto;
        }
        else
        {
            await _uow.Rollback(cancellationToken);
            throw new Exception("No fue posible crear el registro.");
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Groups.Exist(x => x.Id == Id, cancellationToken))
        {
            await _uow.Groups.Delete(Id, cancellationToken);
            return await _uow.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar ele registro.");
        }
        throw new NullReferenceException("Este Grupo no existe.");
    }

    public async Task<GroupDto?> Update(GroupDto Dto, int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Groups.Exist(x => x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<GroupDto, Group>(Dto);
            await _uow.Groups.Update(entity, cancellationToken);
            if (await _uow.Commit(cancellationToken))
            {
                return Dto;
            }
            else
            {
                await _uow.Rollback(cancellationToken);
                throw new Exception("No fue posible actualizar el registro.");
            }
        }
        throw new Exception("El registro que esta intentando actualizar no existe.");
    }

    public async Task<GroupDto> Find(int Id, CancellationToken cancellationToken)
    {
        if (await _uow.Groups.Exist(x => x.Id == Id, cancellationToken))
        {
            var select = await _uow.Groups.Where(x => x.Id == Id, cancellationToken);

            var item = select.ToArray()[0];
            return _mapper.Map<Group, GroupDto>(item);
        }
        throw new Exception("Este registro no existe.");
    }

    public async Task<IBasePagination<GroupDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken)
    {
        Expression<Func<Group, bool>> expression = x => x.Id > 0;

        if (!string.IsNullOrEmpty(Search) && !string.IsNullOrWhiteSpace(Search))
        {
            expression = expression.And(x => x.Name.Contains(Search));
        }

        var query = await _uow.Groups.Where(expression, cancellationToken);
        return query.Select(x => _mapper.Map<Group, GroupDto>(x)).Paginate(page, size);
    }
}
