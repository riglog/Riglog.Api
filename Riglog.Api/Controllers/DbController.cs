using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riglog.Api.Services.Interfaces;


namespace Riglog.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class DbController : ControllerBase
{
    private readonly IDbService _dbService;
    private readonly ISeedService _seedService;

    public DbController(IDbService dbService, ISeedService seedService)
    {
        _dbService = dbService;
        _seedService = seedService;
    }

    /// <summary>
    /// Check pending database migrations
    /// </summary>
    /// <response code="200">No pending migrations</response>
    /// <response code="202">Some pending migrations</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(List<string>))]
    [HttpGet("check")]
    public async Task<IActionResult> MigrationsCheck()
    {
        var migrations = await _dbService.GetMigrationsAsync();

        return migrations.Any() ? Accepted(migrations) : Ok();
    }

    /// <summary>
    /// Apply database migrations
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPatch("update")]
    public async Task<IActionResult> Migrate()
    {
        await _dbService.MigrateAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Seed values
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        try
        {
            await _seedService.SeedOs();
            return Ok();
        }
        catch (Exception e)
        {
            Debug.Write(e);
            return Forbid();
        }
    }
    
    /// <summary>
    /// Create superadmin user
    /// </summary>
    /// <param name="adminPassword">Password</param>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("seed/superadmin")]
    public async Task<IActionResult> CreateSuperAdmin([FromBody] string adminPassword)
    {
        try
        {
            await _seedService.SeedAdminUserAsync(adminPassword);
            return Ok();
        }
        catch (Exception)
        {
            return Forbid();
        }
    }
}