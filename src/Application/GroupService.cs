using Domain;
using System.Linq.Expressions;

namespace Application;

public interface IGroupService : IGenericService<Group, int>
{
   //
}

public class GroupService : IGroupService
{
   private readonly IGenericRepository<Group, int> _repository;

   public GroupService(IGenericRepository<Group, int> Repository)
   {
      _repository = Repository;
   }
   public Group Add(Group Entity)
   {
      _repository.Add(Entity);
      _repository.Commit();
      return Entity;
   }


   public void Delete(int Id)
   {
      var item = _repository.Get(Id);

      if (item is null) throw new NullReferenceException("Este Grupo no existe.");

      _repository.Delete(Id);
      _repository.Commit();
   }
   public void Update(Group Entity, int Id)
   {
      var entity = _repository.Get(Id);

      if (entity is null) throw new NullReferenceException("Este Grupo no existe.");

      Entity.Id = Id;
      entity = Entity;
      _repository.Update(entity);
      _repository.Commit();
   }

   public IList<Group> GetAll(int? skip, int? take) => _repository.GetAll(skip, take).ToList();

   public IList<Group> Filter(Expression<Func<Group, bool>> predicate, int? skip, int? take)
   {
      return _repository.Where(predicate, skip, take).ToList();
   }
}
