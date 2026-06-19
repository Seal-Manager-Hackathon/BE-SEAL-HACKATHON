# Staff get topics by track

## Tác dụng
Staff lấy danh sách topic của một track cụ thể (bao gồm cả topic đã bị disable), phục vụ quản lý và gán topic cho team.

## URL
`GET /api/v1/staff/tracks/{trackId}/topics`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Có | Id của track cần lấy danh sách topic. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên topic. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/staff/tracks/00000000-0000-0000-0000-000000000000/topics?keyword=blockchain&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": null,
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "id": "guid",
        "trackId": "guid",
        "title": "string",
        "description": "string|null",
        "maxTeam": 0,
        "isDisable": false,
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
- Chỉ Staff/Admin mới có quyền truy cập.
- Track phải tồn tại, nếu không trả `TRACK_NOT_FOUND`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
