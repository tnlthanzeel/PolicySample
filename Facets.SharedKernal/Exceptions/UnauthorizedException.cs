namespace Facets.SharedKernal.Exceptions;

public sealed class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}
