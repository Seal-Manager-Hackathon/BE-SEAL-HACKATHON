using Microsoft.AspNetCore.Mvc;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using SystemsService = Hackathon.Service.Systems;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class SystemController : ControllerBase
{
    private readonly SystemsService.IService _systemService;
    private readonly IWebHostEnvironment _environment;
    private static readonly DateTime StartupTime = DateTime.UtcNow;

    public SystemController(SystemsService.IService systemService, IWebHostEnvironment environment)
    {
        _systemService = systemService;
        _environment = environment;
    }

    [HttpGet("enums")]
    public IActionResult GetEnums()
    {
        var data = _systemService.GetEnums();
        return Ok(ApiResponseFactory.Base(data, 200, "GET_SYSTEM_ENUMS_SUCCESSFUL", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("health")]
    public async Task<IActionResult> GetHealth()
    {
        var data = await _systemService.GetHealth(StartupTime);
        return Ok(ApiResponseFactory.Base(data, 200, "GET_SYSTEM_HEALTH_SUCCESSFUL", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        var data = _systemService.GetVersion(_environment.EnvironmentName);
        return Ok(ApiResponseFactory.Base(data, 200, "GET_SYSTEM_VERSION_SUCCESSFUL", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpPost("files/upload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string? folder)
    {
        var result = await _systemService.UploadFile(file, folder);
        return Ok(ApiResponseFactory.Base(result, 200, "FILE_UPLOADED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
