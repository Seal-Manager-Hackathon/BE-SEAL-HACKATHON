// Import marker class dùng để ASP.NET tìm đúng file .resx trong thư mục Resources.
using Hackathon.Api.Resources;
// Import interface + message key constants ở tầng Service để API không hard-code logic dịch.
using Hackathon.Service.Localization;
// Import IStringLocalizer, service chuẩn của ASP.NET Core để đọc resource theo culture hiện tại.
using Microsoft.Extensions.Localization;

// Namespace của phần triển khai localization ở tầng API.
namespace Hackathon.Api.Localization;

// sealed vì class này không cần kế thừa; implement IMessageLocalizer để các layer khác gọi qua abstraction.
public sealed class MessageLocalizer : IMessageLocalizer
{
    // IStringLocalizer sẽ tự đọc SharedResource.{culture}.resx dựa trên Accept-Language hiện tại.
    private readonly IStringLocalizer<SharedResource> _localizer;

    // Constructor nhận IStringLocalizer từ Dependency Injection.
    public MessageLocalizer(IStringLocalizer<SharedResource> localizer)
    {
        // Lưu localizer lại để các method Get/GetTitle dùng chung.
        _localizer = localizer;
    }

    // Dịch 1 key/message code thành text theo ngôn ngữ hiện tại.
    public string Get(string? key)
    {
        // Nếu key null/rỗng thì trả chuỗi rỗng để tránh lỗi null khi response serialize.
        if (string.IsNullOrWhiteSpace(key))
        {
            // Không có gì để dịch.
            return string.Empty;
        }

        // Tìm key trong file resource tương ứng culture hiện tại, ví dụ SharedResource.vi.resx.
        var localized = _localizer[key];

        // Nếu không tìm thấy key trong .resx thì fallback về key gốc để API cũ không bị vỡ.
        return localized.ResourceNotFound ? key : localized.Value;
    }

    // Dịch title của lỗi; ưu tiên key dạng CODE_TITLE, fallback theo status code.
    public string GetTitle(string? key, int statusCode)
    {
        // Nếu có key thì thử lấy title riêng bằng hậu tố _TITLE; nếu không có key thì dùng status code.
        var titleKey = string.IsNullOrWhiteSpace(key) ? StatusCodeToKey(statusCode) : $"{key}_TITLE";

        // Tìm titleKey trong resource theo culture hiện tại.
        var localized = _localizer[titleKey];

        // Nếu không có CODE_TITLE thì fallback sang title chung theo HTTP status code.
        return localized.ResourceNotFound ? Get(StatusCodeToKey(statusCode)) : localized.Value;
    }

    // Map HTTP status code sang message key mặc định để title/error luôn có text dễ hiểu.
    private static string StatusCodeToKey(int statusCode) => statusCode switch
    {
        // 400: request sai dữ liệu/format.
        StatusCodes.Status400BadRequest => MessageKeys.BadRequest,
        // 401: chưa đăng nhập/token không hợp lệ.
        StatusCodes.Status401Unauthorized => MessageKeys.Unauthorized,
        // 403: đã đăng nhập nhưng không đủ quyền.
        StatusCodes.Status403Forbidden => MessageKeys.Forbidden,
        // 404: không tìm thấy resource.
        StatusCodes.Status404NotFound => MessageKeys.NotFound,
        // Các lỗi còn lại fallback về lỗi hệ thống.
        _ => MessageKeys.UnexpectedError
    };
}
