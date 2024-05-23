using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF;

public sealed class GroupMap : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(i => i.Id);
        builder.HasMany(x => x.Tasks);
        builder.Property<int>("CreatedById");
        builder.Property<DateTime>("CreatedOn");
        builder.Property<bool>("IsDeleted");
        builder.Property<bool>("IsReadOnly");
        builder.Property<int>("ModifiedById");
        builder.Property<DateTime>("ModifiedOn");
    }
}
