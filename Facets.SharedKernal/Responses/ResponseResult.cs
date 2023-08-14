using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Responses;

public sealed class ResponseResult : ResponseResult<object>
{
    public ResponseResult() : base(default(object))
    {
        Success = true;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base(validationFailures) { }

    public ResponseResult(IList<KeyValuePair<string, IEnumerable<string>>> validationFailures) : base(validationFailures) { }

    public ResponseResult(ApplicationException ex) : base(ex) { }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();
}
