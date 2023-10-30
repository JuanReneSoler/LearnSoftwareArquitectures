using Domain;
using System.Linq.Expressions;

namespace Application;

public interface IPersonService : IGenericService<Person, int>
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

   public Person Add(Person Entity)
   {
      _repository.Add(Entity);
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

   public IList<Person> Filter(Expression<Func<Person, bool>> predicate, int? skip, int? take)
   {
      return _repository.Where(predicate, skip, take).ToList();
   }

   public IList<Person> GetAll(int? skip, int? take)
   {
      return _repository.GetAll(skip, take).ToList();
   }

   public void Update(Person Entity, int Id)
   {
      var entity = _repository.Get(Id);

      if (entity is null) throw new NullReferenceException("Esta Persona no existe.");

      Entity.Id = Id;
      entity = Entity;
      _repository.Update(entity);
      _repository.Commit();
   }
}
