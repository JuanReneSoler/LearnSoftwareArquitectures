using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework;

public sealed class GroupMap : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(i => i.Id);
        builder.HasMany(x => x.Tasks);

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
