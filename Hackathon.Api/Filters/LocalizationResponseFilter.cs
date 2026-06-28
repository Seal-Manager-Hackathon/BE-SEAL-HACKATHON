using Hackathon.Service.Localization;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hackathon.Api.Filters;

// Filter này chạy ngay trước khi API trả response, dùng để dịch Message/Title theo Accept-Language.
public sealed class LocalizationResponseFilter : IResultFilter
{
    // Service dịch message code sang text trong file .resx.
    private readonly IMessageLocalizer _localizer;

    // DI inject IMessageLocalizer để filter không cần biết chi tiết đọc .resx như nào.
    public LocalizationResponseFilter(IMessageLocalizer localizer)
    {
        _localizer = localizer;
    }

    // Method này chạy trước khi ASP.NET serialize object thành JSON response.
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Chỉ xử lý response dạng object JSON; các dạng khác như file/empty result thì bỏ qua.
        if (context.Result is not ObjectResult objectResult)
        {
            return;
        }

        // Chỉ dịch các response model chuẩn của hệ thống.
        switch (objectResult.Value)
        {
            // Success response: dịch field Message, ví dụ SUCCESS -> Thành công.
            case BaseResponse baseResponse:
                baseResponse.Message = _localizer.Get(baseResponse.Message);
                break;
            // Error response: dịch Title và Message, nhưng không đổi MessageCode.
            case ErrorResponse errorResponse:
                errorResponse.Title = _localizer.Get(errorResponse.Title);
                errorResponse.Message = _localizer.Get(errorResponse.Message);
                break;
        }
    }

    // Không cần xử lý sau khi response đã được ghi ra.
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
