namespace Facets.Core.Common.Dtos;

public sealed class FileDto
{
    public required string Id { get; init; }
    public string FileName { get; init; } = null!;
    public string UniqueName { get; init; } = null!;
    public string URI { get; init; } = null!;
    public Guid RelatedEntityId { get; set; }
}
