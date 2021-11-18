using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
        
    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
        
    public async Task<string> LoginAsync(string username, string password)
    {
        var hasher = new PasswordHasher<User>();
        var user = await _userRepository.GetByUsernameAsync(username);
                
        if (hasher.VerifyHashedPassword(user, user.Password, password) == 0)
        {
            throw new Exception();
        }
                
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthSettings:SecretKey")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new(ClaimTypes.PrimarySid, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Username),
            new(ClaimTypes.Role, user.IsSuperAdmin ? "Admin" : "User")
        };

        var tokenOptions = new JwtSecurityToken(
            _configuration.GetValue<string>("AuthSettings:Audience"),
            _configuration.GetValue<string>("AuthSettings:Audience"),
            claims,
            expires: DateTime.Now.AddMonths(1),
            signingCredentials: signinCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}