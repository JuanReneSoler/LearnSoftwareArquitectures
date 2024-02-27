using Application.Dtos;
using Domain;
using System.Linq.Expressions;

namespace Application;

public interface IPersonService : IGenericService<PersonDto, int>
{
    //
}

public class PersonService : IPersonService
{
    private readonly IGenericRepository<Person, int> _repository;

    public PersonService(IGenericRepository<Person, int> Repository)
    {
        _repository = Repository;
    }

    public PersonDto? Add(PersonDto Entity)
    {
        var entity = new Person
        {
            Name = Entity.Name,
        };
        _repository.Add(entity);
        if (_repository.Commit())
        {
            Entity.Id = entity.Id;
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(PersonDto);
        }
    }

    public int Delete(int Id)
    {
        var entity = _repository.Get(Id);

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        _repository.Delete(Id);
        return _repository.Commit() ? Id : 0;
    }

    public IList<PersonDto> Filter(Expression<Func<PersonDto, bool>> predicate, int? skip, int? take)
    {
        var result = _repository.GetAll(skip, take).Select(x => new PersonDto
        {
            Id = x.Id,
            Name = x.Name,
        });
        return result.Where(predicate).ToList();
    }

    public IList<PersonDto> GetAll(int? skip, int? take)
    {
        return _repository.GetAll(skip, take).Select(x => new PersonDto
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }

    public PersonDto? Update(PersonDto Entity, int Id)
    {
        var entity = _repository.Get(Id);

        if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

        entity.Name = Entity.Name;
        _repository.Update(entity);
        if (_repository.Commit())
        {
            Entity.Id = entity.Id;
            return Entity;
        }
        else
        {
            _repository.Rollback();
            return default(PersonDto);
        }
    }
}
