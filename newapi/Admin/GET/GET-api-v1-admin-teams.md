# Lấy danh sách nhóm toàn hệ thống (Admin Get Teams)

## Tác dụng
Cho phép Admin xem và tìm kiếm danh sách toàn bộ các team đã được khởi tạo trong hệ thống.

## URL
`GET /api/v1/admin/teams`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

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
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "name": "Chiến binh công nghệ",
        "canEdit": true,
        "isDisable": false,
        "createdAt": "2026-06-21T10:00:00Z",
        "memberCount": 5
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Trả về danh sách team có phân trang, hỗ trợ lọc theo disable status.
- Hiển thị tổng số thành viên hoạt động (`memberCount`) của từng team.
- Sắp xếp mặc định theo thời gian tạo nhóm giảm dần (`CreatedAt` giảm dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Yêu cầu quyền Admin để thực hiện hành động này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Tài khoản không có vai trò Admin. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
