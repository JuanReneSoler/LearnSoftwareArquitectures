using Domain.Base;

namespace Domain;

public class Work : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public int PersonId { get; set; }
    
    public Person Person { get; set; }
    public Group Group { get; set; }
}
