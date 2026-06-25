# Tìm kiếm Track hệ thống (Search Tracks)

## Tác dụng
Lấy danh sách track, hỗ trợ tìm kiếm, lọc và phân trang toàn hệ thống.

## URL
`GET /api/v1/tracks`

## Authorization
Không yêu cầu Access Token (Public API).

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Không | Lọc các track thuộc một event cụ thể. |
| `keyword` | `string` | Không | Tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`. |
| `isDisable` | `bool` | Không | `true` để lấy cả track đã disable, `false` chỉ lấy track đang hoạt động (mặc định: `false`). |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định: `1`). |
| `pageSize` | `int` | Không | Số lượng track trên một trang (mặc định: `10`). |

## Ví dụ request
```http
GET /api/v1/tracks?eventId=a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d&keyword=web&pageIndex=1&pageSize=10
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "title": "Bảng A - Web Application",
        "description": "Phát triển Web.",
        "maxTeam": 50,
        "isDisable": false,
        "createdAt": "2026-06-21T08:00:00Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Nếu truyền `eventId`, event đó phải tồn tại và đang hoạt động, chỉ lọc các track thuộc event này.
- Kết quả được sắp xếp tăng dần theo `Title` của Track.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
