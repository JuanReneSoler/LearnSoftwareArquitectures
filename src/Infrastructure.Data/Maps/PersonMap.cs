using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Maps;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Groups);
        builder.HasMany(x => x.Tasks);
    }
}
