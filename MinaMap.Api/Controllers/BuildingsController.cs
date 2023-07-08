using Application.Dtos.Building.Request;
using Application.Handlers.Building.Commands;
using Application.Handlers.Building.Queries;
using Microsoft.AspNetCore.Mvc;
using MinaMap.Api.Controllers.Base;

namespace MinaMap.Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BuildingsController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdBuildingAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new GetByIdBuildingQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAllBuildingAsync()
        => Ok(await Mediator.Send(new GetAllBuildingQuery()));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBuildingAsync([FromRoute] int id, [FromForm] UpdateBuildingRequestDto request)
        => Ok(await Mediator.Send(new UpdateBuildingCommand(id, request.File)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuildingAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new DeleteBuildingCommand(id)));

    [HttpPost]
    public async Task<IActionResult> CreateBuildingAsync([FromForm] CreateBuildingRequestDto request)
        => Ok(await Mediator.Send(new CreateBuildingCommand(request.File)));

    [HttpGet("getPoi/{id}")]
    public async Task<IActionResult> GetPoiAsync(int id)
        => Ok(await Mediator.Send(new GetByIdPoiQuery(id)));
}
