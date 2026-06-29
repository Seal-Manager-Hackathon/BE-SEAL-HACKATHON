using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CriticalService = Hackathon.Service.Criticals;

namespace Hackathon.Api.Controllers;

[ApiController]
public class CriticalController : ControllerBase
{
    private readonly CriticalService.IService _criticalService;

    public CriticalController(CriticalService.IService criticalService)
    {
        _criticalService = criticalService;
    }

    [HttpGet("api/v1/rounds/{roundId}/criteria")]
    public async Task<IActionResult> GetCriteriaByRound(Guid roundId)
    {
        var result = await _criticalService.GetCriteriaByRound(roundId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("api/v1/events/{eventId}/criteria")]
    public async Task<IActionResult> GetCriteriaByEvent(Guid eventId)
    {
        var result = await _criticalService.GetCriteriaByEvent(eventId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPost("/api/v1/admin/events/{eventId:guid}/rounds/{roundId:guid}/criteria")]
    public async Task<IActionResult> CreateCriteria(Guid eventId, Guid roundId, CriticalService.Request.CreateCriteriaRequest request)
    {
        var result = await _criticalService.CreateCriteria(eventId, roundId, request);
        return Ok(ApiResponseFactory.Base(result, 201, "CRITERIA_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/events/{eventId:guid}/rounds/{roundId:guid}/criteria/{templateId:guid}/activate")]
    public async Task<IActionResult> ActivateCriteria(Guid eventId, Guid roundId, Guid templateId)
    {
        await _criticalService.ActivateCriteria(eventId, roundId, templateId);
        return Ok(ApiResponseFactory.Base(null, 200, "CRITERIA_ACTIVATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("/api/v1/admin/events/{eventId:guid}/rounds/{roundId:guid}/criteria")]
    public async Task<IActionResult> GetCriteriaTemplatesByRound(Guid eventId, Guid roundId)
    {
        var result = await _criticalService.GetCriteriaTemplatesByRound(eventId, roundId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }
}
