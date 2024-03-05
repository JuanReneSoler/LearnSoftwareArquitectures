namespace Domain.Base;

public interface ISoftDelete<TEntityID>
{

    TEntityID CreatedById { get; set; }
    DateTime CreatedOn { get; set; }
    bool IsDeleted { get; set; }
    bool IsReadOnly { get; set; }
    TEntityID ModifiedById { get; set; }
    DateTime ModifiedOn { get; set; }
}
