using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using RolesService = Hackathon.Service.Roles;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/roles")]
public class RolesController : ControllerBase
{
    private readonly RolesService.IService _rolesService;

    public RolesController(RolesService.IService rolesService)
    {
        _rolesService = rolesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _rolesService.GetRoles();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("event-roles")]
    public async Task<IActionResult> GetEventRoles()
    {
        var result = await _rolesService.GetEventRoles();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }
}
