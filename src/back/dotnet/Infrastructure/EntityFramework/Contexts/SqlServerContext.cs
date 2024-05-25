using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public sealed class SqlServerContext : DbContext
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

        modelBuilder.ApplyConfiguration(new GroupMap());
        modelBuilder.ApplyConfiguration(new PersonMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
        Filter<Group>(modelBuilder);
        Filter<Person>(modelBuilder);
        Filter<Tasks>(modelBuilder);
        Filter<Group>(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void HandleSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted && e.Entity is ISoftDelete<int>))
        {
            entry.State = EntityState.Modified;
            entry.CurrentValues["IsDeleted"] = true;
        }
    }

    public override int SaveChanges()
    {
        HandleSoftDelete();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleSoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void Filter<T>(ModelBuilder modelBuilder)
        where T : BaseEntity<int>
    {
        modelBuilder.Entity<T>().HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
    }
}
