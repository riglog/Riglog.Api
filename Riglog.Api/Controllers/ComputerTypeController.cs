using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Riglog.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ComputerTypeController : ControllerBase
{

    public ComputerTypeController()
    {

    }
}