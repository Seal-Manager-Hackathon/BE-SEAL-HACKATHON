# Admin Get Teams

## Tác dụng
Cho phép Admin xem và tìm kiếm danh sách toàn bộ các team đã được khởi tạo trong hệ thống.

## URL
`GET /api/v1/admin/teams`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm team theo tên nhóm.
    *   `isDisable` (bool, Không bắt buộc): `true` để lấy các team bị soft-delete/disable, `false` để lấy team đang active.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
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
        "name": "Chiến binh công nghệ",
        "canEdit": true,
        "isDisable": false,
        "createdAt": "2026-06-21T10:00:00Z",
        "memberCount": 5
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
- Trả về danh sách team có phân trang, hỗ trợ lọc theo disable status.
- Hiển thị tổng số thành viên hoạt động (`memberCount`) của từng team.
- Sắp xếp mặc định theo thời gian tạo nhóm giảm dần (`CreatedAt` giảm dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "ADMIN_ROLE_REQUIRED",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_ROLE_REQUIRED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
