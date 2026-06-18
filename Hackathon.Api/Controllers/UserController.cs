using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService = Hackathon.Service.Users;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService.IService _userService;

    public UserController(UserService.IService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfileUser()
    {
        var result = await _userService.GetProfileUser();
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
