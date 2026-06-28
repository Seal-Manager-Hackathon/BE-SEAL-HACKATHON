# Get my teams

## Tác dụng
Lấy danh sách phân trang các team mà người dùng hiện tại đang tham gia (trong bảng `TeamDetails`).

## URL
`GET /api/v1/teams/me`

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

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
        "teamId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Chiến binh công nghệ",
        "canEdit": true,
        "isLeader": true,
        "memberStatus": 0, /* 0: Active, 1: Inactive */
        "joinedAt": "2026-06-22T08:00:00Z"
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
- Yêu cầu đăng nhập với role `Student`.
- Chỉ hiển thị các team mà người dùng hiện tại đang tham gia và đang còn hoạt động (`Status = Active` trong bảng `TeamDetails`, team và thành viên không bị disable).
- Sắp xếp danh sách theo thời gian tạo team: Team nào được tạo mới hơn sẽ lên trước (`Team.CreatedAt` giảm dần).

### Bảng trạng thái thành viên TeamDetailStatusEnum (Integer)
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Active | Thành viên chính thức hoạt động |
| `1` | Inactive | Thành viên đã rời nhóm hoặc ngưng hoạt động |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
