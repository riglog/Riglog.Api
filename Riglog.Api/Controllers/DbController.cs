using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data;

namespace Riglog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DbController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DbController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var migrations = await _dbContext.Database.GetPendingMigrationsAsync();

            return migrations.Any() ? Accepted(migrations) : Ok();
        }

        /// <summary>
        /// Apply database migrations
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("update")]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            return Ok();
        }
        
    }
}