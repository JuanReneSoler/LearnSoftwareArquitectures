using Application.Dtos;
using Domain.Models;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface IPersonService : IGenericService<PersonDto>
{
}

public sealed class PersonService : IPersonService
{
    private readonly IGenericRepository<Person> _repository;
    private readonly IMapper _mapper;

    public PersonService(
            IGenericRepository<Person> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
    }

    public async Task<PersonDto?> Create(PersonDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PersonDto, Person>(Dto);
        await _repository.Add(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(PersonDto);
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, cancellationToken)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        await _repository.Delete(Id, cancellationToken);
        return await _repository.Commit(cancellationToken) ? Id : 0;
    }

    public async Task<IBasePagination<PersonDto>> Filter(Expression<Func<PersonDto, bool>> predicate, int page, int size, CancellationToken cancellationToken)
    {
        var query = await _repository.Select(x => new PersonDto
        {
            Id = x.Id,
            Name = x.Name
        }, predicate, cancellationToken);
        return query.Paginate(page, size);
    }

    public async Task<PersonDto?> Update(PersonDto Dto, int Id, CancellationToken cancellationToken)
    {
        var entity = (await _repository.Where(x => x.Id == Id, cancellationToken)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        entity.Name = Dto.Name;
        await _repository.Update(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback(cancellationToken);
            return default(PersonDto);
        }
    }
}
