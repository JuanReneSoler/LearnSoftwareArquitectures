using Domain; 
using Domain.Models;
using System.Linq.Expressions;

namespace Application;

public interface IGroupService : IGenericService<Group>
{
   //
}

public class GroupService : IGroupService
{
   private readonly IGenericRepository<Group> _repository;

   public GroupService(IGenericRepository<Group> Repository)
   {
      _repository = Repository;
   }
    public Group Add(Group Entity)
    {
       _repository.Add(Entity);
       _repository.Save();
       return Entity;
    }


    public void Delete(int Id)
    {
       var item = _repository.Get(Id);

       if( item is null ) throw new NullReferenceException("Este Grupo no existe.");

       _repository.Delete(Id);
       _repository.Save();
    }
    public void Update(Group Entity, int Id)
    {
       var entity = _repository.Get(Id);

       if(entity is null) throw new NullReferenceException("Este Grupo no existe.");

       Entity.Id = Id;
       entity = Entity;
       _repository.Update(entity);
       _repository.Save();
    }

    public IList<Group> GetAll()=> _repository.GetAll();

    public IList<Group> Filter(Expression<Func<Group, bool>> predicate)
    {
       return _repository.Where(predicate);
    }
}
