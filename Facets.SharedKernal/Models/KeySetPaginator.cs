
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Models;

public sealed class KeysetPaginator
{
    [JsonIgnore]
    public bool Next => Reference is null ? true : Reference.Split(':').Last() is "1";
    public string? Reference { get; set; }

    [JsonIgnore]
    public Guid? ReferenceId => Reference is null ? null : Guid.Parse(Reference.Split(':').First());

    public int PageSize { get; set; }

    [JsonIgnore]
    public bool IsFirstPage => Reference is null;


}
