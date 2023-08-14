using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Responses;

public sealed class KeySetResponseResult : KeySetResponseResult<object>
{
    public KeySetResponseResult() : base(default(object))
    {
        Success = true;
    }

    public KeySetResponseResult(IList<ValidationFailure> validationFailures) : base(validationFailures) { }

    public KeySetResponseResult(IList<KeyValuePair<string, IEnumerable<string>>> validationFailures) : base(validationFailures) { }

    public KeySetResponseResult(ApplicationException ex) : base(ex) { }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();
}
