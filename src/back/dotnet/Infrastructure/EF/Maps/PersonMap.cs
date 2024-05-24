using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF;

public sealed class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Groups);
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
