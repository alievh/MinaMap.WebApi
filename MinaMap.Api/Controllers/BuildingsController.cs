using Microsoft.AspNetCore.Mvc;

namespace MinaMap.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BuildingsController : ControllerBase
{
}
