# Get mentor notifications

## Tác dụng
Lấy danh sách thông báo mentor gửi trong các track được phân công.

## URL
`GET /api/mentor-notifications`

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
GET /api/mentor-notifications?eventId=00000000-0000-0000-0000-000000000000&trackId=00000000-0000-0000-0000-000000000000&pageIndex=1&pageSize=10
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
        "eventId": "guid",
        "eventName": "string",
        "trackId": "guid",
        "trackName": "string",
        "mentorId": "guid",
        "mentorName": "string|null",
        "title": "string",
        "description": "string|null",
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
- Mentor notification bị soft-disable không được trả về.
- Nếu user là mentor, chỉ trả notification thuộc track mà mentor được phân công.
- Nếu user là student/team member, chỉ trả notification thuộc track/event mà team của user tham gia theo rule hệ thống.
- Nếu truyền `eventId`, lọc theo event.
- Nếu truyền `trackId`, lọc theo track.
- Kết quả sắp xếp theo `CreatedAt` giảm dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | MENTOR_NOTIFICATION_NOT_VISIBLE_TO_USER | User cannot view these mentor notifications. |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
