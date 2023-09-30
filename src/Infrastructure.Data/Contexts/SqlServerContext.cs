using Domain;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Infrastructure.Data.Maps;

namespace Infrastructure.Data.Contexts;

public class SqlServerContext : DbContext
{
    private readonly string _stringConnection;

    public SqlServerContext() : base()
    {
        _stringConnection = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
    }

    public SqlServerContext(string stringConnection) : base()
    {
        _stringConnection = stringConnection;
    }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Work> Tasks { get; set; }
    public DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_stringConnection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GroupMap());
        modelBuilder.ApplyConfiguration(new PersonMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
    }
}
