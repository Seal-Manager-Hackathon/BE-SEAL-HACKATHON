# Judge chấm điểm thử (Judge Submit Mock Score)

## Tác dụng
Cho phép Judge hoặc Admin nhập điểm chấm thử/chấm nháp của bài thi (không tính vào điểm số thăng vòng chính thức).

## URL
`POST /api/v1/judge/submissions/{submissionId}/scores/mock`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Judge phụ trách.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | ID của bài nộp. |

## Request Body
```json
{
  "totalScore": 75.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 20.0,
      "comment": "Chấm nháp thử nghiệm hệ thống."
    }
  ]
}
```

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
  "message": "MOCK_SCORE_SUBMITTED",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalScore": 75.0,
    "isRetake": false,
    "isMock": true,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 20.0,
        "comment": "Chấm nháp thử nghiệm hệ thống."
      }
    ]
  }
}
```

## Business rules
- Ghi nhận bản ghi điểm trong DB với cờ `IsMock = true` để phân biệt hoàn toàn với điểm thi đấu chính thức.
- Điểm mock này sẽ bị bỏ qua khi BTC chạy API kết thúc round và tính toán điểm trung bình thăng vòng cho các team.
- **Judge gọi API**: Nếu caller là Judge, phải kiểm tra Judge được phân công track chứa submission (giống BR của POST scores chính thức). Nếu không, trả 403 FORBIDDEN.
- **Multiple mock scores**: Cho phép tạo nhiều mock scores cho cùng một submission. Mỗi lần gọi tạo bản ghi mới, không ghi đè.
- **Conflict với real score**: Cho phép tạo mock score ngay cả khi submission đã có real score. Mock score và real score độc lập với nhau.

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
| 400 | SCORE_LIMIT_EXCEEDED | Điểm chấm cho tiêu chí lớn hơn điểm tối đa cho phép. |
| 400 | SCORE_TOTAL_MISMATCH | Tổng điểm chi tiết không khớp với totalScore gửi lên. |
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Thiếu quyền Judge phụ trách. |
| 404 | SUBMISSION_NOT_FOUND | Bài thi không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `POST /api/v1/judge/submissions/{submissionId}/scores/mock`.
- Sử dụng policy `LecturerPolicy`.
