# Get mentor notifications

## Tác dụng
Lấy danh sách thông báo mentor gửi trong các track được phân công, dành cho mentor hoặc team member.

## URL
`GET /api/v1/mentor-notifications`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc thông báo theo event. |
| `trackId` | `guid` | Không | Lọc thông báo theo track. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/mentor-notifications?eventId=00000000-0000-0000-0000-000000000000&trackId=00000000-0000-0000-0000-000000000000&pageIndex=1&pageSize=10
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
        "assignTrackId": "guid",
        "trackId": "guid",
        "eventId": "guid",
        "title": "string",
        "description": "string|null",
        "createdAt": "datetimeoffset"
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
- Chỉ trả notification chưa bị soft-disable, thuộc assign track và track và event chưa bị disable.
- User phải là mentor được phân công track hoặc là team member có team đăng ký event đó.
- Nếu truyền `eventId`, lọc theo event.
- Nếu truyền `trackId`, lọc theo track.
- Kết quả sắp xếp theo `CreatedAt` giảm dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | MENTOR_NOTIFICATION_NOT_VISIBLE_TO_USER (chỉ khi có filter eventId hoặc trackId) |
| 400 | BAD_REQUEST | Query parameter không hợp lệ (pageIndex/pageSize). |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
