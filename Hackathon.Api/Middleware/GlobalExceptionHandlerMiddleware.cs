using Hackathon.Service.Exceptions;
using Hackathon.Service.Localization;
using Hackathon.Service.Models;

namespace Hackathon.Middleware;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    // Localizer dùng để dịch Title/Message của error response theo Accept-Language.
    private readonly IMessageLocalizer _localizer;

    public GlobalExceptionHandlerMiddleware(
        IHostEnvironment environment,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IMessageLocalizer localizer)
    {
        _environment = environment;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred while processing request {Path}", 
                            context.Request.Path);

            if (context.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started, the global exception middleware will not write an error response");
                throw;
            }
            
            context.Response.ContentType = "application/json";
            
            AppException appEx = ex switch
            {
                AppException alreadyAppEx => alreadyAppEx,
                _ => new ServerException("An unexpected system error occurred.") 
            };
            
            context.Response.StatusCode = appEx.StatusCode;
            
            // Lỗi 5xx không trả message thật ra client; chỉ trả key hệ thống an toàn để dịch.
            var messageKey = appEx.StatusCode >= 500 ? MessageKeys.UnexpectedError : appEx.MessageCode;

            var response = ApiResponseFactory.Error(
                // Dịch title theo CODE_TITLE nếu có, nếu không thì fallback theo status code.
                title: _localizer.GetTitle(messageKey, appEx.StatusCode),
                status: appEx.StatusCode,
                // Dịch message theo messageKey đã chọn ở trên.
                message: _localizer.Get(messageKey),
                // MessageCode vẫn giữ code gốc để FE xử lý logic ổn định.
                messageCode: appEx.MessageCode,
                errors: _environment.IsDevelopment() ? new { detail = ex.Message } : null,
                traceId: context.TraceIdentifier
            );
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
