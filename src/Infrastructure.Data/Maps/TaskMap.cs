using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Maps;

public class TaskMap : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.ToTable("Task");
        builder.HasKey(x=>x.Id);
        builder.HasOne(x=>x.Group);
        builder.HasOne(x=>x.Person);
    }
}
