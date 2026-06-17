using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using CriteriaTemplatesService = Hackathon.Service.CriteriaTemplates;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/rounds")]
public class CriteriaTemplatesController : ControllerBase
{
    private readonly CriteriaTemplatesService.IService _criteriaTemplatesService;

    public CriteriaTemplatesController(CriteriaTemplatesService.IService criteriaTemplatesService)
    {
        _criteriaTemplatesService = criteriaTemplatesService;
    }

    [HttpGet("{roundId}/criteria")]
    public async Task<IActionResult> GetCriteriaByRound(Guid roundId, [FromQuery] bool? isDisable)
    {
        var result = await _criteriaTemplatesService.GetCriteriaByRound(roundId, isDisable);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
