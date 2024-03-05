using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Maps;

namespace Infrastructure.Data.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Group>? Groups { get; set; }
    public DbSet<Tasks>? Tasks { get; set; }
    public DbSet<Person>? People { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    {
    }

    public SqlServerContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Tasks;Trusted_Connection=false;User Id=sa;Password=Linux@1993;Persist Security Info=False;Encrypt=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GroupMap());
        modelBuilder.ApplyConfiguration(new PersonMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
    }
}
