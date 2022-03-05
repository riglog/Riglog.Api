using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services;

public class SeedService : ISeedService
{
    private readonly IUserRepository _userRepository;
    private readonly IOsVersionRepository _osVersionRepository;

    public SeedService(
        IUserRepository userRepository, 
        IOsVersionRepository osVersionRepository
    )
    {
        _userRepository = userRepository;
        _osVersionRepository = osVersionRepository;
    }
        
    public async Task SeedAdminUserAsync(string adminPassword)
    {
        const string adminUser = "riglogadmin";

        var guid = new Guid();
            
        var admin = new User
        {
            Id = guid,
            Username = adminUser,
            FirstName = "Riglog",
            LastName = "Admin",
            Email = "admin@riglog.cz",
            CreatedBy = guid,
            UpdatedBy = guid,
            IsSuperAdmin = true
        };
        await _userRepository.CreateAsync(admin);
            
        var hasher = new PasswordHasher<User>();
        admin.Password = hasher.HashPassword(admin, adminPassword);
            
        await _userRepository.UpdateAsync(admin);
    }

    public Task SeedOsTypes()
    {
        throw new NotImplementedException();
    }

    public Task SeedOsEditions()
    {
        throw new NotImplementedException();
    }
    
    
    public async Task SeedOsVersions()
    {
        var versions = new List<OsVersion>
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "10"
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "11"
            }
        };

        foreach (var version in versions)
        {
            try
            {
                await _osVersionRepository.CreateAsync(version);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}