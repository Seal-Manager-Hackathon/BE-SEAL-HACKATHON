using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MentorsService = Hackathon.Service.Mentors;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/mentor")]
public class MentorsController : ControllerBase
{
    private readonly MentorsService.IService _mentorsService;

    public MentorsController(MentorsService.IService mentorsService)
    {
        _mentorsService = mentorsService;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetMentorEvents([FromQuery] MentorsService.Request.GetMentorEventsRequest request)
    {
        var result = await _mentorsService.GetMentorEvents(request);
        return Ok(result);
    }
}
