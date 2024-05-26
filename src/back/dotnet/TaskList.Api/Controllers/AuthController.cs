using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskList.Api.Dtos;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn([FromBody] LoginModel model, CancellationToken cancellationToken)
    {
        return await Task.Run<IActionResult>(() =>
        {
            if (model.User != "jsoler" && model.Password != "123")
            {
                return Unauthorized();
            }

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, model.User)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }, cancellationToken);
    }
}
