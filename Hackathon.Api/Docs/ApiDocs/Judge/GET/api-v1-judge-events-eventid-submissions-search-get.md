# Judge tìm kiếm submissions theo tên team (Judge Search Submissions)

## Tác dụng
Giúp Judge tìm kiếm các team/submissions theo tên team trong event. Hỗ trợ lọc theo track và phân trang.

## URL
`GET /api/v1/judge/events/{eventId}/submissions/search`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo track cụ thể. |
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên team. |
| `pageIndex` | `int` | Không | Số trang (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` (phân trang).*
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
        "registerTeamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "topicTitle": "Hệ thống quản lý y tế thông minh"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 5,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- `keyword` tìm kiếm không phân biệt hoa thường theo tên team.
- Nếu không truyền `keyword`, trả về tất cả team trong event.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Route: `GET /api/v1/judge/events/{eventId:guid}/submissions/search`.
- Sử dụng policy `LecturerPolicy`.
