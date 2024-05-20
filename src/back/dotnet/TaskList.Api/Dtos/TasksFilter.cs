using System.ComponentModel.DataAnnotations;

namespace TaskList.Api.Dtos;

public class TasksFilter
{
    public int GroupId { get; set; }

    public int PersonId { get; set; }

    public string Search { get; set; } = string.Empty;

    [Required]
    public int page { get; set; }

    [Required]
    public int size { get; set; }

    public CancellationToken cancellationToken { get; set; }
}

