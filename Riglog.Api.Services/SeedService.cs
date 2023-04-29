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
    private readonly IComputerTypeRepository _computerTypeRepository;

    public SeedService(
        IUserRepository userRepository, 
        IOsVersionRepository osVersionRepository, 
        IOsEditionRepository osEditionRepository, 
        IOsDistributionRepository osDistributionRepository, 
        IOsFamilyRepository osFamilyRepository,
        IComputerTypeRepository computerTypeRepository
        )
    {
        _userRepository = userRepository;
        _osVersionRepository = osVersionRepository;
        _osEditionRepository = osEditionRepository;
        _osDistributionRepository = osDistributionRepository;
        _osFamilyRepository = osFamilyRepository;
        _computerTypeRepository = computerTypeRepository;
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
    
    
    public async Task SeedOperatingSystemsAsync()
    {
        var json = await new StreamReader("./Jsons/operating_systems.json").ReadToEndAsync();
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
            
            if (osFamily.OsDistributions is null) continue;
            
            foreach (var osDistribution in osFamily.OsDistributions)
            {
                Guid osDistributionId;
                try
                {
                    osDistributionId = (await _osDistributionRepository.GetByNameAsync(osFamilyId, osDistribution.Name)).Id;
                }
                catch (Exception)
                {
                    osDistribution.OsFamilyId = osFamilyId;
                    osDistributionId = await _osDistributionRepository.CreateAsync(osDistribution);
                }

                if (osDistribution.OsEditions is not null)
                {
                    foreach (var osEdition in osDistribution.OsEditions)
                    {
                        try
                        {
                            await _osEditionRepository.GetByNameAsync(osDistributionId, osEdition.Name);
                        }
                        catch (Exception)
                        {
                            osEdition.OsDistributionId = osDistributionId;
                            await _osEditionRepository.CreateAsync(osEdition);
                        }
                    }
                }

                if (osDistribution.OsVersions is null) continue;
                
                foreach (var osVersion in osDistribution.OsVersions)
                {
                    try
                    {
                        await _osVersionRepository.GetByNameAsync(osDistributionId, osVersion.Name);
                    }
                    catch (Exception)
                    {
                        osVersion.OsDistributionId = osDistributionId;
                        await _osVersionRepository.CreateAsync(osVersion);
                    }
                }
            }
        }
    }

    public async Task SeedComputerTypesAsync()
    {
        var json = await new StreamReader("./Jsons/computer_types.json").ReadToEndAsync();
        var computerTypes = JsonSerializer.Deserialize<List<ComputerType>>(json);
        
        if (computerTypes is null) return;

        foreach (var computerType in computerTypes)
        {
            try
            {
                await _computerTypeRepository.GetByNameAsync(computerType.Name);
            }
            catch (Exception)
            {
                await _computerTypeRepository.CreateAsync(computerType);
            }
        }
    }
}