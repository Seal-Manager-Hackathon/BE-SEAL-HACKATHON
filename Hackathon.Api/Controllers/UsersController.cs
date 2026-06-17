using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService = Hackathon.Service.Users;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly UsersService.IService _usersService;

    public UsersController(UsersService.IService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _usersService.GetProfile();
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("profile")]
    public async Task<IActionResult> UpdateProfile(UsersService.Request.UpdateProfileRequest request)
    {
        var result = await _usersService.UpdateProfile(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
