namespace Facets.Core.Common.Dtos;

public sealed class FileUploadedResponse
{
    public string BlobName { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string UniqueName { get; set; } = null!;
    public string URI { get; set; } = null!;
}
