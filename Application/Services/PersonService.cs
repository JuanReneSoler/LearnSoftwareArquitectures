using Domain;
using Domain.Models;
using System.Linq.Expressions;

namespace Application;

public interface IPersonService : IGenericService<Person>
{
   //
}

public class PersonService : IPersonService
{
   private readonly IGenericRepository<Person> _repository;

   public PersonService(IGenericRepository<Person> Repository)
   {
      _repository = Repository;
   }

   public Person Add(Person Entity)
   {
      _repository.Add(Entity);
      _repository.Save();
      return Entity;
   }

   public void Delete(int Id)
   {
      var entity = _repository.Get(Id);

      if(entity is null) throw new NullReferenceException("Esta Persona no existe.");

      _repository.Delete(Id);
      _repository.Save(); 
   }

    public IList<Person> Filter(Expression<Func<Person, bool>> predicate)
    {
       return _repository.Where(predicate);
    }

    public IList<Person> GetAll()
    {
       return _repository.GetAll();
    }

    public void Update(Person Entity, int Id)
    {
       var entity = _repository.Get(Id);

       if(entity is null) throw new NullReferenceException("Esta Persona no existe.");

       Entity.Id = Id;
       entity = Entity;
       _repository.Update(entity);
       _repository.Save();
    }
}
