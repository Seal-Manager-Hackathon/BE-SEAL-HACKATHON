# Judge xem dashboard chấm thi (Judge Scoring Dashboard)

## Tác dụng
Giúp Judge xem danh sách thống kê toàn bộ các bài thi mình đã chấm, chưa chấm, và tổng số bài thi cần chấm của các bảng đấu được phân công trong giải đấu.

## URL
`GET /api/v1/judge/scores/me`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên với vai trò Judge.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": {
    "totalAssignedSubmissions": 15,
    "totalGradedSubmissions": 10,
    "totalPendingSubmissions": 5,
    "gradedPercentage": 66.67
  }
}
```

## Business rules
- Người gọi phải là giảng viên có vai trò Judge đang hoạt động.
- Tính toán dữ liệu:
  - `totalAssignedSubmissions`: Đếm số bài thi của các team thuộc các track Judge được gán.
  - `totalGradedSubmissions`: Đếm số bài thi Judge đã chấm (`Scores` tồn tại).
  - `totalPendingSubmissions`: Đếm số bài thi Judge chưa chấm (`Scores` chưa có).
- **Edge case**: Nếu `totalAssignedSubmissions = 0` → `gradedPercentage = 0` (không tính division để tránh lỗi).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Unauthorized",
  "status": 401,
  "message": "Vui lòng xác thực tài khoản.",
  "messageCode": "UNAUTHORIZED",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/scores/me`.
- Sử dụng policy `LecturerPolicy`.
