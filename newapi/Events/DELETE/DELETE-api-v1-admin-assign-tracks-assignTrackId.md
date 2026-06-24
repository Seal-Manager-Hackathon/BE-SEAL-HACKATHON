# Gỡ phân công khỏi Track (Remove Track Assignment)

## Tác dụng
Cho phép BTC gỡ phân công gán bảng đấu (Track) của Mentor hoặc Judge.

## URL
`DELETE /api/v1/admin/assign-tracks/{assignTrackId}`

## Quyền
Admin hoặc Staff (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `assignTrackId` (Guid, Bắt buộc): ID của bản ghi gán track.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TRACK_ASSIGNMENT_REMOVED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi `AssignTracks` phải tồn tại.
- BTC kiểm tra quyền của Staff.
- Đặt cờ `IsDisable = true` cho bản ghi phân công track.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bản ghi gỡ phân công.",
  "MessageCode": "ASSIGN_TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không được phân công phụ trách. |
| 404 | ASSIGN_TRACK_NOT_FOUND | Bản ghi gán track không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
