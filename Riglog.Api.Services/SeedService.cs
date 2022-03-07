using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Services.Interfaces;

namespace Riglog.Api.Services;

public class SeedService : ISeedService
{
    private readonly IUserRepository _userRepository;
    private readonly IOsFamilyRepository _osFamilyRepository;
    private readonly IOsDistributionRepository _osDistributionRepository;
    private readonly IOsEditionRepository _osEditionRepository;
    private readonly IOsVersionRepository _osVersionRepository;

    public SeedService(
        IUserRepository userRepository, 
        IOsVersionRepository osVersionRepository, 
        IOsEditionRepository osEditionRepository, 
        IOsDistributionRepository osDistributionRepository, 
        IOsFamilyRepository osFamilyRepository
        )
    {
        _userRepository = userRepository;
        _osVersionRepository = osVersionRepository;
        _osEditionRepository = osEditionRepository;
        _osDistributionRepository = osDistributionRepository;
        _osFamilyRepository = osFamilyRepository;
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
    
    
    public async Task SeedOs()
    {
        var json = await new StreamReader("./Seeds/os.json").ReadToEndAsync();
        var operatingSystems = JsonSerializer.Deserialize<List<OsFamily>>(json);
        
        if (operatingSystems is null) return;
        
        foreach (var osFamily in operatingSystems)
        {
            Guid osFamilyId;
            try
            {
                osFamilyId = (await _osFamilyRepository.GetByNameAsync(osFamily.Name)).Id;
            }
            catch (Exception)
            {
                osFamilyId = await _osFamilyRepository.CreateAsync(osFamily);
            }
        }
    }
}