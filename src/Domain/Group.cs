using Domain.Base;

namespace Domain;

public class Group : BaseEntity
{
    public String Name { get; set; }

    public ICollection<Work> Tasks { get; set; }
}
