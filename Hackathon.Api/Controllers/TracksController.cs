using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/tracks")]
public class TracksController : ControllerBase
{
    private readonly TracksService.IService _tracksService;

    public TracksController(TracksService.IService tracksService)
    {
        _tracksService = tracksService;
    }


}
