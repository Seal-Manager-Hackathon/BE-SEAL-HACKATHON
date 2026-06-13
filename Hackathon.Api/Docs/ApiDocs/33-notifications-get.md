# Get notifications

## Tác dụng
Lấy danh sách notification của user hiện tại.

## URL
`GET /api/notifications`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `status` | `string` | Không | Lọc theo trạng thái notification, ví dụ `Unread`, `Read`. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/notifications?status=Unread&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "id": "guid",
        "teamId": "guid|null",
        "title": "string",
        "description": "string|null",
        "status": "string|null",
        "createdAt": "datetimeoffset",
        "updatedAt": "datetimeoffset"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 0,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả notification thuộc user hiện tại.
- Notification bị soft-disable không được trả về.
- Nếu truyền `status`, lọc theo trạng thái hợp lệ của notification.
- Kết quả sắp xếp theo `CreatedAt` giảm dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
