namespace Domain;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public string ModifiedById { get; set; }
    public DateTime ModifiedOn { get; set; }
}
