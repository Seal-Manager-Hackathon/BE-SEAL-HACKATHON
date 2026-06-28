using Hackathon.Api.Extention;
using Hackathon.Service.Admin.Request;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminService = Hackathon.Service.Admin;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.AdminPolicy)]
[Route("api/v1/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService.IService _adminService;

    public AdminController(AdminService.IService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _adminService.GetAllUsers(paginationRequest);
        return Ok(result);
    }

    [HttpGet("users/search")]
    public async Task<IActionResult> SearchUsers([FromQuery] GetUsersQuery query)
    {
        var result = await _adminService.SearchUsers(query);
        return Ok(result);
    }
}
