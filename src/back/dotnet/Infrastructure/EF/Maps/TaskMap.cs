using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF;

public sealed class TaskMap : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.ToTable("Task");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Group);
        builder.HasOne(x => x.Person);
        
        builder.Property<int>("CreatedById")
            .HasDefaultValue(null);
        
        builder.Property<DateTime>("CreatedOn")
            .HasDefaultValueSql("GETDATE()");
        
        builder.Property<bool>("IsDeleted")
            .HasDefaultValue(false);
        
        builder.Property<bool>("IsReadOnly")
            .HasDefaultValue(false);
        
        builder.Property<int>("ModifiedById")
            .HasDefaultValue(null);
        
        builder.Property<DateTime>("ModifiedOn")
            .HasDefaultValue(null);
    }
}
