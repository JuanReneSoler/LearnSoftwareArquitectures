using Domain;
using Domain.Models;
using System.Linq.Expressions;

namespace Application;

public interface IWorkService : IGenericService<Work>
{
   void ReasignToGroup(int WorkId, int GroupId);
}

public class WorkService : IWorkService
{
   private readonly IGenericRepository<Work> _repository;
   
   public WorkService(IGenericRepository<Work> Repository)
   {
      _repository = Repository;
   }

    public Work Add(Work Entity)
    {
       _repository.Add(Entity);
       _repository.Save();
       return Entity;
    }

    public void Delete(int Id)
    {
       var entity = _repository.Get(Id);

       if(entity is null) throw new NullReferenceException("Esta Tarea no existe.");

       _repository.Delete(Id);
       _repository.Save(); 
    }

    public void Update(Work Entity, int Id)
    {
       var entity = _repository.Get(Id);
         
       if(entity is null) throw new NullReferenceException("Esta Persona no existe.");


       Entity.Id = Id;
       entity = Entity;
       _repository.Update(entity);
       _repository.Save();
    }

    public IList<Work> GetAll()=> _repository.GetAll();

    public IList<Work> Filter(Expression<Func<Work, bool>> predicate)
    {
       var result = _repository.Where(predicate);
       return result.ToList();
    }

   public void ReasignToGroup(int WorkId, int GroupId)
   {
      var _work = _repository.Get(WorkId);
      _work.GroupId = GroupId;
      _repository.Save();
   }
}
