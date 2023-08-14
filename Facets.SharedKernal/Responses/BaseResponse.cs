namespace Facets.SharedKernal.Responses;

public abstract class BaseResponse
{
    public bool Success { get; protected init; }

    public virtual List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();

    public BaseResponse()
    {
        Success = false;
    }
}
