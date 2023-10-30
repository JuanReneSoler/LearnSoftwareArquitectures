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

   public PersonDto Add(PersonDto Entity)
   {
      var entity = new Person
      {
         Name = Entity.Name,
      };
      _repository.Add(entity);
      _repository.Commit();
      return Entity;
   }

   public void Delete(int Id)
   {
      var entity = _repository.Get(Id);

      if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

      _repository.Delete(Id);
      _repository.Commit();
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

   public void Update(PersonDto Entity, int Id)
   {
      var entity = _repository.Get(Id);

      if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

      entity.Name = Entity.Name;
      _repository.Update(entity);
      _repository.Commit();
   }
}
