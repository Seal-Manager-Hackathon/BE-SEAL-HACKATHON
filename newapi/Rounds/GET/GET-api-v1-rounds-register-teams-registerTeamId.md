# API 54: Lấy chi tiết Vòng thi đang tham gia (Get Round Detail)

## Tác dụng
Lấy thông tin chi tiết của vòng thi mà team đang tham gia, bao gồm các mốc thời gian thi đấu, bảng đấu (track) và đề bài (topic) được giao.

## URL
`GET /api/v1/rounds/register-teams/{registerTeamId}`

## Quyền
Authenticated User (Yêu cầu đăng nhập, dành cho thành viên của team)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerTeamId` (Guid, Bắt buộc): ID đơn đăng ký tham gia sự kiện của team.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "roundName": "Vòng loại",
    "roundNo": 1,
    "description": "Nộp sản phẩm đề tài tự chọn.",
    "startTime": "2026-07-01T08:00:00Z",
    "endTime": "2026-07-01T18:00:00Z",
    "startSubmission": "2026-07-01T08:00:00Z",
    "endSubmission": "2026-07-01T17:30:00Z",
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Bảng A - Web Application",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Hệ thống quản lý y tế thông minh"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Đơn đăng ký thi đấu `registerTeamId` phải tồn tại trong DB và ở trạng thái đã được BTC duyệt (`Approved`), không bị soft-disable.
- Trả ra đầy đủ các trường thông tin cấu hình của vòng đấu hiện tại mà team đang tham gia kèm bảng đấu và đề thi đã gán.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin đơn đăng ký thi của đội.",
  "MessageCode": "REGISTER_TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | REGISTER_TEAM_NOT_FOUND | Đơn đăng ký không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
