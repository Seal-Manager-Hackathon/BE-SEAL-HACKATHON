# Judge nhập điểm thi (Judge Submit Score)

## Tác dụng
Giúp Judge nhập điểm tổng, điểm chi tiết theo tiêu chí chấm điểm và nhận xét/feedback cho bài thi.

## URL
`POST /api/v1/judge/submissions/{submissionId}/scores`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên được phân công Judge phụ trách track.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | ID của bài nộp thi. |

## Request Body
```json
{
  "totalScore": 85.5,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 25.5,
      "comment": "Ý tưởng tốt."
    }
  ]
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa chi tiết bảng điểm vừa tạo.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SCORE_SUBMITTED_SUCCESSFULLY",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalScore": 85.5,
    "isRetake": false,
    "isMock": false,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 25.5,
        "comment": "Ý tưởng tốt."
      }
    ]
  }
}
```

## Business rules
- Bài thi `submissionId` phải tồn tại và chưa bị soft-delete.
- Giám khảo gọi API phải được phân công chấm bảng đấu có bài thi này (đối chiếu qua `AssignTracks`).
- Hệ thống kiểm tra: Điểm chấm cho từng tiêu chí (`score` trong mảng request body) không được lớn hơn điểm tối đa (`maxScore`) của tiêu chí đó cấu hình trong DB.
- Tự động cộng tổng điểm (hoặc xác thực tính toán tổng điểm gửi từ client khớp với tổng điểm chi tiết).
- Lưu thông tin điểm số vào bảng `Scores` và các điểm chi tiết vào bảng `ScoreItems`. Hai hành động này bắt buộc bọc trong cùng một **Database Transaction**.
- Giám khảo không được phép chấm điểm lần hai nếu đã chấm rồi (nếu đã có bản ghi điểm của giám khảo này đối với bài thi, cấm tạo mới mà phải gọi API `PATCH` để sửa điểm - BR-SCO-06).
- **`totalScore` server validation**: Server kiểm tra tổng `scores[].score` khớp với `totalScore`. Nếu không khớp, trả `400 SCORE_TOTAL_MISMATCH`.
- **Mock → Real**: Nếu submission chỉ có mock score (chưa có real score), cho phép tạo real score. Bản ghi real và mock độc lập nhau.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Conflict",
  "status": 409,
  "message": "Bạn đã cho điểm bài thi này trước đó. Vui lòng cập nhật thay vì tạo mới.",
  "messageCode": "SCORE_ALREADY_EXISTS",
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
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Giám khảo không có quyền chấm bài thi của bảng này. |
| 404 | SUBMISSION_NOT_FOUND | Bài thi không tồn tại. |
| 409 | SCORE_ALREADY_EXISTS | Giám khảo đã chấm bài này rồi, yêu cầu dùng PATCH để sửa điểm. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `POST /api/v1/judge/submissions/{submissionId}/scores`.
- Sử dụng policy `LecturerPolicy`.
