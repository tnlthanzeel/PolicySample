namespace Facets.SharedKernal.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public List<KeyValuePair<string, IEnumerable<string>>> ValdationErrors { get; set; } = new();

    public ValidationException(List<KeyValuePair<string, string>> validationResult)
    {

        foreach (var validationError in validationResult)
        {
            ValdationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(validationError.Key, new[] { validationError.Value }));
        }
    }
}
