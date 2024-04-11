using Domain.Entities;
using System.Linq.Expressions;
using Application.Dtos;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface IGroupService : IGenericService<GroupDto>
{
}

public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group> _repository;
    private readonly IMapper _mapper;

    public GroupService(
            IGenericRepository<Group> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
    }

    public GroupDto? Create(GroupDto Dto)
    {
        var group = _mapper.Map<GroupDto, Group>(Dto);
        _repository.Add(group);
        if (_repository.Commit())
        {
            Dto.Id = group.Id;
            return Dto;
        }
        else
        {
            _repository.Rollback();
            return default(GroupDto);
        }
    }

    public int Delete(int Id)
    {
        var item = _repository.Where(x => x.Id == Id, null, null).FirstOrDefault();

        if (item is null) throw new NullReferenceException("Este Grupo no existe.");

        _repository.Delete(Id);
        return _repository.Commit() ? Id : 0;
    }

    public GroupDto? Update(GroupDto Dto, int Id)
    {
        var entity = _repository.Where(x => x.Id == Id, null, null).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Este Grupo no existe.");

        entity.Name = Dto.Name;
        _repository.Update(entity);
        if (_repository.Commit())
        {
            return Dto;
        }
        else
        {
            _repository.Rollback();
            return default(GroupDto);
        }
    }

    public IList<GroupDto> Filter(Expression<Func<GroupDto, bool>> predicate, int? skip, int? take)
    {
        var result = _repository.Where(x => x.Id > 0, skip, take).Select(x => new GroupDto
        {
            Id = x.Id,
            Name = x.Name
        });

        return result.Where(predicate).ToList();
    }
}
