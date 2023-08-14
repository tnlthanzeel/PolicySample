namespace Facets.Core.Security.Dtos;

public sealed record ResetPasswordDto
{
    public string Token { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
}
