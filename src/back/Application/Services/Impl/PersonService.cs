using Application.Dtos;
using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;

namespace Application.Services;

public interface IPersonService : IGenericService<PersonDto>
{
}

public class PersonService : IPersonService
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

    public async Task<PersonDto?> Create(PersonDto Dto)
    {
        var entity = _mapper.Map<PersonDto, Person>(Dto);
        await _repository.Add(entity);
        if (await _repository.Commit())
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback();
            return default(PersonDto);
        }
    }

    public async Task<int> Delete(int Id)
    {
        var entity = (await _repository.Where(x => x.Id == Id, null, null)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        await _repository.Delete(Id);
        return await _repository.Commit() ? Id : 0;
    }

    public async Task<IList<PersonDto>> Filter(Expression<Func<PersonDto, bool>> predicate, int? skip, int? take)
    {
        var result = (await _repository.Where(x => x.Id > 0, skip, take)).Select(x => new PersonDto
        {
            Id = x.Id,
            Name = x.Name,
        });
        return result.Where(predicate).ToList();
    }

    public async Task<PersonDto?> Update(PersonDto Dto, int Id)
    {
        var entity = (await _repository.Where(x => x.Id == Id, null, null)).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        entity.Name = Dto.Name;
        await _repository.Update(entity);
        if (await _repository.Commit())
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        else
        {
            await _repository.Rollback();
            return default(PersonDto);
        }
    }
}
