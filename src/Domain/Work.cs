namespace Domain;

public class Work : IBaseEntity, ISoftDelete
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public int PersonId { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public string ModifiedById { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Person Person { get; set; }
    public Group Group { get; set; }
}
