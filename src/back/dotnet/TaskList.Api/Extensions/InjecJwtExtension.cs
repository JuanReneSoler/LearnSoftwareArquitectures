using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskList.Api.Extensions;

public static class InjectJwtExtention
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
    {
        var key = config["Jwt:Key"] ?? string.Empty;
        services.AddAuthorization();
        services.AddAuthentication("Bearer").AddJwtBearer(opt =>
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = signingKey,
            };
        });
        return services;
    }
}
