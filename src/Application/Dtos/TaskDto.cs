using Application.Base;

namespace Application.Dtos;

public class TaskDto:DtoBase<int>
{
   public string Title { get; set; }
   public string Description { get; set; }
   public int GroupId { get; set; }
   public GroupDto Group { get; set; }
   public int PersonId { get; set; }
   public PersonDto Person { get; set; }
}
