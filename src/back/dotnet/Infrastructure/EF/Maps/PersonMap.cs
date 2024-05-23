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
        builder.Property<int>("CreatedById");
        builder.Property<DateTime>("CreatedOn");
        builder.Property<bool>("IsDeleted");
        builder.Property<bool>("IsReadOnly");
        builder.Property<int>("ModifiedById");
        builder.Property<DateTime>("ModifiedOn");
    }
}
