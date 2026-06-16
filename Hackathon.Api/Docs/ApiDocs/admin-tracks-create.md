# Admin create tracks

## Tác dụng
Admin tạo mới các Bảng đấu (Track) nằm trong một sự kiện cụ thể.

## URL
`POST /api/admin/events/{eventId}/tracks`

## Authorization
Yêu cầu access token hợp lệ và role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id event cần tạo track. |

## Request body
```json
{
  "name": "string",
  "description": "string|null"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "eventId": "guid",
    "name": "string",
    "description": "string|null",
    "message": "TRACK_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Admin được tạo track.
- Event phải tồn tại và chưa bị soft-disable.
- Tên track là bắt buộc.
- Tên track không được trùng trong cùng một event.
- Track được dùng để phân bảng đấu, assign mentor/judge và chứa topics.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_REQUIRED |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 400 | BAD_REQUEST | TRACK_NAME_REQUIRED |
| 409 | CONFLICT | TRACK_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
