# Judge khóa điểm số (Judge Finalize Score)

## Tác dụng
Giúp Judge khóa bảng điểm số của mình (chuyển trạng thái sang finalized để gửi chính thức kết quả chấm cho BTC).

## URL
`POST /api/v1/judge/scores/{scoreId}/finalize`

## Quyền
Judge sở hữu bảng điểm (Yêu cầu đăng nhập tài khoản Giảng viên chấm thi)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `scoreId` (Guid, Bắt buộc): ID của bảng điểm.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "SCORE_FINALIZED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bảng điểm phải tồn tại trong DB, người gọi phải là chủ nhân bảng điểm.
- Đánh dấu trạng thái finalized (khóa bảng điểm, cấm sửa đổi trực tiếp qua API `PATCH /scores/{id}`).
- Thông báo tới hệ thống quản lý thăng hạng để cập nhật tiến trình chấm. *DB hiện chưa thiết lập trường IsFinalized cho bảng Scores.*

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không có quyền quản lý bảng điểm này.",
  "MessageCode": "SCORE_NOT_OWNED_BY_JUGDE",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | SCORE_NOT_OWNED_BY_JUGDE | Bảng điểm không thuộc về người gọi. |
| 404 | SCORE_NOT_FOUND | Bảng điểm không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
