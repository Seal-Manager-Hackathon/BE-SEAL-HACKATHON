# BTC gán giải thưởng trên Leaderboard (Admin Assign Award)

## Tác dụng
Cho phép BTC gán giải thưởng đạt được (`LevelAward`) và điều chỉnh điểm số thủ công cho một team cụ thể trên leaderboard.

## URL
`PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`

## Quyền
Admin hoặc Staff (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `leaderBoardId` (Guid, Bắt buộc): ID của Leaderboard.
    *   `teamId` (Guid, Bắt buộc): ID của team cần gán giải.

## Request Body
```json
{
  "score": 275.0,
  "levelAward": "Giải Nhất"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "AWARD_ASSIGNED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi `LeaderBoardDetails` liên kết `leaderBoardId` và `teamId` phải tồn tại.
- Cập nhật trường `Score` và `LevelAward` tương ứng trong bảng `LeaderBoardDetails` (BR-LB-06).
- Chỉ cho phép chỉnh sửa khi sự kiện và leaderboard chưa bị khóa (read-only).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin xếp hạng của đội.",
  "MessageCode": "LEADERBOARD_DETAIL_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không được phân công gán quyền quản trị sự kiện này. |
| 404 | LEADERBOARD_DETAIL_NOT_FOUND | Bản ghi xếp hạng của team không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
