using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Riglog.Api.Configurations;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IConfiguration _configuration;
        
    public ConfigureJwtBearerOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }
        
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _configuration.GetValue<string>("AuthSettings:Audience"),
            ValidAudience = _configuration.GetValue<string>("AuthSettings:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthSettings:SecretKey") ?? string.Empty))
        };
    }
        
    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}