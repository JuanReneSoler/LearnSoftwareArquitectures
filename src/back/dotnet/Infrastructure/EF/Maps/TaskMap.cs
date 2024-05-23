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
        builder.Property<int>("CreatedById");
        builder.Property<DateTime>("CreatedOn");
        builder.Property<bool>("IsDeleted");
        builder.Property<bool>("IsReadOnly");
        builder.Property<int>("ModifiedById");
        builder.Property<DateTime>("ModifiedOn");
    }
}
