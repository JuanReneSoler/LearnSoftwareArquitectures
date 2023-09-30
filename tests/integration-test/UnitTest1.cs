using Application.Services;
using Domain;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;

namespace integration_test;

[TestClass]
public class UnitTest1
{
    private readonly SqlServerContext _context;
    public UnitTest1()
    {
      try
      {
          _context = new SqlServerContext(@"Data Source=localhost;Initial Catalog=Tasks;Integrated Security=False;User Id=sa;Password=Linux@1993;");
          _context.Database.EnsureCreated();
      }
      catch (System.Exception)
      {
          
          throw;
      }
    }

    [TestMethod]
    public void TestGroupService()
    {
        var repository = new GenericRepository<Group>(_context);
        var groupService = new GroupService(repository);
    }

    [TestMethod]
    public void TestPersonService()
    {
        var repository = new GenericRepository<Person>(_context);
        var personService = new PersonService(repository);
    }

    [TestMethod]
    public void TestTaskService()
    {
        var repository = new GenericRepository<Work>(_context);
        var taskService = new TaskService(repository);
    }
}
