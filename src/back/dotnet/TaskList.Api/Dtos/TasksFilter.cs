using System.ComponentModel.DataAnnotations;

namespace TaskList.Api.Dtos;

public class TasksFilter
{
    public int GroupId { get; set; }

    public int PersonId { get; set; }

    [Required]
    public int page { get; set; }

    [Required]
    public int size { get; set; }

    public CancellationToken cancellationToken { get; set; }
}

