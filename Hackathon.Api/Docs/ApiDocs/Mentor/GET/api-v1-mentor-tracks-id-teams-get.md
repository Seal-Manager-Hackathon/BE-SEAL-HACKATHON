# Mentor xem danh sách các team trong bảng đấu

## Tác dụng
Giúp Mentor xem danh sách tất cả các đội thi (team) thuộc bảng đấu (Track) mình được phân công phụ trách. Team nào chọn/được gán vào track này thì thuộc phạm vi mentor đảm nhiệm.

## URL
`GET /api/v1/mentor/tracks/{trackId}/teams`

## Quyền
Mentor phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track cần lấy danh sách team.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang (tối đa 100).

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách team thi đấu (phân trang).*
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
        "topicTitle": "Hệ thống quản lý y tế thông minh",
        "leaderName": "Hoàng Phạm",
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
- Mentor gọi API phải được phân công phụ trách track thi đấu này (đối chiếu qua bảng `AssignTracks`), nếu không từ chối và báo lỗi `FORBIDDEN`.
- Trả về danh sách các team đã chọn/được gán track thi đấu này trong bảng `RegisterTeams` và đơn đăng ký được duyệt (`Status = Approved`).
- Mentor chỉ quản lý/xem các team thuộc track mình được phân công; không xem hoặc quản lý team ở track khác.
- Mỗi team trả về thêm `registerTeamId` để có thể truy xuất chi tiết đăng ký.
- Hỗ trợ phân trang qua `pageIndex` và `pageSize`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "FORBIDDEN",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công phụ trách track này. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
