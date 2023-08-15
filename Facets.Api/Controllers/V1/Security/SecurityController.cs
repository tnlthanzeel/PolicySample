using Facets.Core.Security;
using Facets.Core.Security.Dtos;
using Facets.Core.Security.Interfaces;
using Facets.Core.Security.ModulePermissions;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Facets.Api.Controllers.V1.Security;

[Route("api/security")]
public sealed class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly JwtConfig _jwtConfig;

    public SecurityController(ISecurityService securityService, IOptions<JwtConfig> jwtConfig)
    {
        _securityService = securityService;
        _jwtConfig = jwtConfig.Value; 
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(ResponseResult<AuthenticatedUserDto>), StatusCodes.Status200OK)]
    public ActionResult Authenticate([FromBody] AuthenticateUserDto model, CancellationToken token)
    {
        var key = Encoding.ASCII.GetBytes(_jwtConfig.SigningKey);

        var tokenDescriptor = new JwtSecurityToken
        (
        issuer: _jwtConfig.Issuer,
        audience: _jwtConfig.Audience,
        expires: DateTime.Now.AddDays(30),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        claims: new Claim[] { new Claim("permission", "event.view") }
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return Ok(new { jwtToken });
    }
}
