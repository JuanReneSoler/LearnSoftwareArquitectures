using Domain.Base;

namespace Domain.Entities;

public class Group : BaseEntity<int>
{
    public String Name { get; set; }

    public ICollection<Tasks> Tasks { get; set; }
}
