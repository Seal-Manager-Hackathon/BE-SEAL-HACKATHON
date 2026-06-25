# Danh sách đề bài quản lý (Admin Get Track Topics)

## Tác dụng
Cho phép Admin/Staff xem danh sách đầy đủ tất cả các đề bài (Topic) của một bảng đấu kể cả đề thi đang bị ẩn hoặc disable.

## URL
`GET /api/v1/admin/tracks/{trackId}/topics`

## Authorization
Yêu cầu Access Token của tài khoản Staff hoặc Admin (BTC).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của Track cần quản lý đề. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `keyword` | `string` | Không | Tìm kiếm đề thi theo từ khóa. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái disable. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định: `1`). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: `10`). |

## Ví dụ request
```http
GET /api/v1/admin/tracks/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d/topics?pageIndex=1&pageSize=10
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
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "title": "Hệ thống số hóa y tế",
        "description": "Xây dựng ứng dụng quản lý.",
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
- Track được tra cứu phải tồn tại.
- BTC kiểm tra quyền của Staff đối với sự kiện tương ứng (phải được phân công quản lý).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
