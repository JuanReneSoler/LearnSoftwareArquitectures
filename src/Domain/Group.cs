namespace Domain;

public class Group : IBaseEntity, ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Work> Tasks { get; set; }

    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public string ModifiedById { get; set; }
    public DateTime ModifiedOn { get; set; }
}
