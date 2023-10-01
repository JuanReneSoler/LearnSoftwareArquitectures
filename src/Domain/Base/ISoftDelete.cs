public interface ISoftDelete
{
    DateTime CreatedOn { get; set; }
    bool IsDeleted { get; set; }
    bool IsReadOnly { get; set; }
    string ModifiedById { get; set; }
    DateTime ModifiedOn { get; set; }
}
