# Đếm số lượng event team đã tham gia (Approved)

## Tác dụng
API dùng để đếm số lượng các sự kiện (events) mà một team cụ thể đã được ban tổ chức chấp nhận tham gia (đơn đăng ký có trạng thái là `Approved`).

## URL
`GET /api/v1/teams/{teamId}/events/approved-count`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `teamId` | `guid` | Có | ID của Team cần đếm. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/teams/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d/events/approved-count
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(...)`. Value trả về là số lượng đếm được.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "count": 2
  }
}
```

## Business rules
- Team phải tồn tại và đang không bị soft-disable (`IsDisable = false`).
- Chỉ đếm các đơn đăng ký (`RegisterTeams`) của `teamId` có `Status = Approved` và `IsDisable = false`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
