using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hackathon.Service.Models;
using LecturersService = Hackathon.Service.Lecturers;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/lecturers")]
public class LecturersController : ControllerBase
{
    private readonly LecturersService.IService _lecturersService;

    public LecturersController(LecturersService.IService lecturersService)
    {
        _lecturersService = lecturersService;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetLecturerEvents([FromQuery] PaginationRequest request)
    {
        var result = await _lecturersService.GetLecturerEvents(request);
        return Ok(result);
    }

    [HttpGet("events/search")]
    public async Task<IActionResult> SearchLecturerEvents([FromQuery] LecturersService.Request.SearchLecturerEventsRequest request)
    {
        var result = await _lecturersService.SearchLecturerEvents(request);
        return Ok(result);
    }
}
