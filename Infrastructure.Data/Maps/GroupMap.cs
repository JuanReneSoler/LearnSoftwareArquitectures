using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GroupMap : IEntityTypeConfiguration<Group>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(i => i.Id);
        builder.HasMany(x=>x.Tasks).WithOne(x=>x.Group);
    }
}
