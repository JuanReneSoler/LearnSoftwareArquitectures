using Domain;
using System.Linq.Expressions;

namespace Application.Services;

public interface ITaskService : IGenericService<Work>
{
   void ReasignToGroup(int TaskId, int GroupId);
}

public class TaskService : ITaskService
{
   private readonly IGenericRepository<Work> _repository;
   
   public TaskService(IGenericRepository<Work> Repository)
   {
      _repository = Repository;
   }

    public Work Add(Work Entity)
    {
       _repository.Add(Entity);
       _repository.Commit();
       return Entity;
    }

    public void Delete(int Id)
    {
       var entity = _repository.Get(Id);

       if(entity is null) throw new NullReferenceException("Esta Tarea no existe.");

       _repository.Delete(Id);
       _repository.Commit(); 
    }

    public void Update(Work Entity, int Id)
    {
       var entity = _repository.Get(Id);
         
       if(entity is null) throw new NullReferenceException("Esta Persona no existe.");


       Entity.Id = Id;
       entity = Entity;
       _repository.Update(entity);
       _repository.Commit();
    }

    public IList<Work> GetAll(int? skip, int? take)=> _repository.GetAll(skip, take);

    public IList<Work> Filter(Expression<Func<Work, bool>> predicate, int? skip, int? take)
    {
       var result = _repository.Where(predicate, skip, take);
       return result.ToList();
    }

   public void ReasignToGroup(int TaskId, int GroupId)
   {
      var _work = _repository.Get(TaskId);
      _work.GroupId = GroupId;
      _repository.Commit();
   }
}
