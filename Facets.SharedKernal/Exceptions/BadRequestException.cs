namespace Facets.SharedKernal.Exceptions;

public sealed class BadRequestException : ApplicationException
{
    public string PropertyName;

    public BadRequestException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}