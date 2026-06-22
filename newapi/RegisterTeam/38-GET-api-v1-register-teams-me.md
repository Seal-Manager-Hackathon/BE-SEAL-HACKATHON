# API 38: Xem đăng ký của tôi (Get My Registered Events)

## Tác dụng
API dùng cho người dùng (Student) xem team của mình đã tham gia (đăng ký) vào những event nào. Chỉ lấy những event mà `RegisterTeam` có trạng thái là `Approved` (đã được chấp nhận).

## URL
`GET /api/v1/register-teams/me`

## Quyền
Student (Yêu cầu đăng nhập tài khoản Student)

## Request Headers
- \`Authorization: Bearer <"AccessToken">\`

## Request Parameters
*   **Query Parameters:**
    *   `status` (string, Bắt buộc, truyền cứng value: `Approved`): Chỉ lấy đơn đăng ký đã duyệt.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "Items": [
      {
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "SEAL Hackathon 2026",
        "Status": 1, /* Approved */
        "description": "Chúng em đăng ký tham gia.",
        "createdAt": "2026-06-22T08:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- Tài khoản phải là `Student` hợp lệ.
- User đang thuộc về những team nào (trạng thái member `Active` và team `IsDisable = false`), API sẽ tìm kiếm các đơn đăng ký của các team đó.
- Lọc theo điều kiện truyền vào `status = Approved` để chỉ trả về các team **đã được staff duyệt chấp nhận**.

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt đăng ký |
| `1` | Approved | Đã duyệt tham gia sự kiện |
| `2` | Rejected | Bị từ chối tham gia sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng xác thực tài khoản.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | CURRENT_USER_MUST_BE_STUDENT | Người gọi không phải học sinh. |
| 400 | BAD_REQUEST | Tham số status không đúng định dạng enum. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
