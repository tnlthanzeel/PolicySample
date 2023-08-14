namespace Facets.Core.Security;

public sealed class JwtConfig
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SigningKey { get; set; } = null!;
    public TimeSpan TokenLifetime { get; set; }
}