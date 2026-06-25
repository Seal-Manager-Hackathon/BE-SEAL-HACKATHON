# Judge chấm điểm phúc khảo (Judge Submit Regrade Score)

## Tác dụng
Giúp Judge tạo một bảng điểm phúc khảo riêng biệt từ bảng điểm cũ của chính mình. Bản ghi mới có `IsRetake = true`, bản ghi cũ được giữ nguyên.

## URL
`POST /api/v1/judge/scores/{scoreId}/retake`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên sở hữu bảng điểm gốc.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `scoreId` | `guid` | Có | ID của bảng điểm gốc cần chấm lại. |

## Request Body
```json
{
  "totalScore": 88.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 28.0,
      "comment": "Chấm lại: Đã xem xét kỹ khiếu nại của thí sinh."
    }
  ]
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` — `scoreId` trong `data` là ID của bảng điểm MỚI (bản ghi phúc khảo), KHÔNG phải `scoreId` trong path.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "REGRADE_SCORE_SUBMITTED",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalScore": 88.0,
    "isRetake": true,
    "isMock": false,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 28.0,
        "comment": "Chấm lại: Đã xem xét kỹ khiếu nại của thí sinh."
      }
    ]
  }
}
```

## Business rules
- `scoreId` trong path là ID của bảng điểm CŨ. API tạo bảng điểm MỚI với cờ `IsRetake = true`, không ghi đè bảng điểm cũ.
- Bảng điểm cũ `scoreId` phải tồn tại trong DB.
- Người gọi phải là Judge sở hữu bảng điểm cũ.
- Không cho phúc khảo điểm mock (`IsMock = true`).
- Chỉ cho tạo một bản ghi phúc khảo active cho cùng `SubmissionId` + `AssignTrackId`.
- Server validate criteria thuộc round của submission, điểm không vượt `maxScore`, và tổng điểm chi tiết khớp `totalScore`.
- Do DB hiện tại chưa có field phê duyệt phúc khảo/assigned judge riêng trong `Reports`, API này không kiểm tra trạng thái phê duyệt phúc khảo từ report.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Conflict",
  "status": 409,
  "message": "Đã có điểm phúc khảo cho bảng điểm này.",
  "messageCode": "SCORE_ALREADY_RETAKEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | MOCK_SCORE_CANNOT_BE_RETAKEN | Không thể phúc khảo điểm mock. |
| 400 | SCORE_LIMIT_EXCEEDED | Điểm chấm cho tiêu chí lớn hơn điểm tối đa cho phép. |
| 400 | SCORE_TOTAL_MISMATCH | Tổng điểm chi tiết không khớp với totalScore gửi lên. |
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | SCORE_NOT_OWNED_BY_JUDGE | Bảng điểm gốc không thuộc về người gọi. |
| 404 | SCORE_NOT_FOUND | Không tìm thấy bảng điểm cũ. |
| 409 | SCORE_ALREADY_RETAKEN | Đã có điểm phúc khảo cho bảng điểm này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `POST /api/v1/judge/scores/{scoreId}/retake`.
- Sử dụng policy `LecturerPolicy`.
