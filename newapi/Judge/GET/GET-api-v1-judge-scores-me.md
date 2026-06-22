# Judge xem dashboard chấm thi (Judge Scoring Dashboard)

## Tác dụng
Giúp Judge xem danh sách thống kê toàn bộ các bài thi mình đã chấm, chưa chấm, và tổng số bài thi cần chấm của các bảng đấu được phân công trong giải đấu.

## URL
`GET /api/v1/judge/scores/me`

## Quyền
Lecturer với vai trò Judge (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin thống kê.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "totalAssignedSubmissions": 15,
    "totalGradedSubmissions": 10,
    "totalPendingSubmissions": 5,
    "gradedPercentage": 66.67
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là giảng viên có vai trò Judge đang hoạt động.
- Tính toán dữ liệu:
  - `totalAssignedSubmissions`: Đếm số bài thi của các team thuộc các track Judge được gán.
  - `totalGradedSubmissions`: Đếm số bài thi Judge đã chấm (`Scores` tồn tại).
  - `totalPendingSubmissions`: Đếm số bài thi Judge chưa chấm (`Scores` chưa có).

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
| 403 | FORBIDDEN | Giảng viên chưa được phân công làm Judge trong bảng đấu nào. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
