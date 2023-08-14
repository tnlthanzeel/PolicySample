namespace Facets.SharedKernal.Interfaces;

public interface IDeletedAudit
{
    DateTimeOffset? DeletedOn { get; set; }

    string? DeletedBy { get; set; }
    public bool IsDeleted { get; }
}
