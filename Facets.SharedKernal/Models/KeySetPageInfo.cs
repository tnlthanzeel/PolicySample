using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Models;

public sealed class KeysetPageInfo
{
    public int TotalRecordCount { get; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }

    public int PageSize { get; }

    private string? _nextPageReference = null;

    public string? NextPageReference
    {
        get
        {
            return _nextPageReference;
        }
        set
        {
            _nextPageReference = value is null ? null : string.Concat(value, ":1");
        }
    }

    private string? _previousPageReference = null;
    public string? PreviousPageReference
    {
        get
        {
            return _previousPageReference;
        }
        set
        {
            _previousPageReference = value is null ? null : string.Concat(value, ":0");
        }
    }

    [JsonIgnore]
    public bool IsFirstPage => NextPageReference is null && PreviousPageReference is null;

    public KeysetPageInfo(int totalRecordCount, int pageSize)
    {
        TotalRecordCount = totalRecordCount;
        PageSize = pageSize;
    }
}
