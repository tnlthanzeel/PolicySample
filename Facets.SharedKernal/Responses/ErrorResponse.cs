namespace Facets.SharedKernal.Responses;

public sealed class ErrorResponse : BaseResponse
{
    public string? TraceId { get; init; }

    public ErrorResponse()
    {
        Success = false;
    }
}
