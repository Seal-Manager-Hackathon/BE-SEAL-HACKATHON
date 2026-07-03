# Lấy lý do từ chối đăng ký tham gia sự kiện (Get Rejection Reason)

## Tác dụng
Lấy lý do từ chối do Admin hoặc Staff nhập khi từ chối đơn đăng ký tham gia sự kiện của team.

## URL
`GET /api/v1/register-teams/{registerId}/rejection-reason`

## Quyền
Authenticated user.

## Path Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerId` | `Guid` | Có | ID của đơn đăng ký tham gia sự kiện (`RegisterTeamId`). |

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*

```json
{
  "isSuccess": true,
  "isFailed": false,
  "value": {
    "registerId": "9f7b8a1c-2d3e-4f5a-9b6c-7d8e9f0a1b2c",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "status": "Rejected",
    "rejectionReason": "Không đủ số lượng thành viên theo yêu cầu của sự kiện."
  },
  "message": "SUCCESS",
  "statusCode": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-23T08:00:00Z"
}
```

## Response Fields
| Trường | Kiểu dữ liệu | Mô tả |
|---|---|---|
| `value.registerId` | `Guid` | ID của đơn đăng ký tham gia sự kiện. |
| `value.teamId` | `Guid` | ID của team đã gửi đơn đăng ký. |
| `value.eventId` | `Guid` | ID của sự kiện team đăng ký tham gia. |
| `value.status` | `RegisterTeamStatusEnum` | Trạng thái hiện tại của đơn đăng ký. |
| `value.rejectionReason` | `string` hoặc `null` | Lý do Admin/Staff nhập khi từ chối đơn đăng ký. Có thể `null` nếu đơn chưa bị từ chối hoặc chưa có lý do được ghi nhận. |
| `message` | `string` | Mã thông báo thành công, hiện tại là `SUCCESS`. |

## Business rules
- `registerId` phải là ID của một đơn đăng ký tồn tại trong hệ thống.
- API trả về thông tin cơ bản của đơn đăng ký và lý do từ chối tương ứng.
- Nếu đơn đăng ký chưa bị từ chối hoặc chưa có lý do, `value.rejectionReason` có thể là `null`.
- User phải đăng nhập để gọi API này.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "Not Found",
  "status": 404,
  "detail": "Không tìm thấy đơn đăng ký.",
  "messageCode": "REGISTER_TEAM_NOT_FOUND",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-23T08:00:00Z"
}
```

### Các mã lỗi cụ thể
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | `registerId` không đúng định dạng `Guid`. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Người dùng không có quyền xem lý do từ chối của đơn đăng ký này. |
| 404 | REGISTER_TEAM_NOT_FOUND | Không tìm thấy đơn đăng ký tương ứng với `registerId`. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
