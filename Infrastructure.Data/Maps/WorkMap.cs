using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class WorkMap : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.ToTable("Task");
        builder.HasKey(x=>x.Id);
        builder.HasOne(x=>x.Group);
        builder.HasOne(x=>x.Person);
    }
}
