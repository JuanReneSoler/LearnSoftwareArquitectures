namespace Application.Dtos;

public class TaskDto : DtoBase<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public int PersonId { get; set; }

    public TaskDto() : base(0)
    {
        this.Title = string.Empty;
        this.Description = string.Empty;
        this.GroupId = 0;
        this.PersonId = 0;
    }
}
