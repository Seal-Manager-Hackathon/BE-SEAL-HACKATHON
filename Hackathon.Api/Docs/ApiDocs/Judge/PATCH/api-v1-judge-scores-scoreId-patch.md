# Judge sửa điểm (Judge Update Score)

## Tác dụng
Giúp Judge sửa lại điểm số tổng hoặc điểm số chi tiết từng tiêu chí đã chấm cho bài thi.

## URL
`PATCH /api/v1/judge/scores/{scoreId}`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên sở hữu bảng điểm.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `scoreId` | `guid` | Có | ID của bảng điểm cần sửa. |

## Request Body
```json
{
  "totalScore": 90.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 30.0,
      "comment": "Chỉnh sửa: Ý tưởng xuất sắc hơn mong đợi."
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
  "message": "SCORE_UPDATED_SUCCESSFULLY",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalScore": 90.0,
    "isRetake": false,
    "isMock": false,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 30.0,
        "comment": "Chỉnh sửa: Ý tưởng xuất sắc hơn mong đợi."
      }
    ]
  }
}
```

## Business rules
- Bảng điểm `scoreId` phải tồn tại trong DB.
- Người gọi phải chính là Judge đã tạo ra bảng điểm này.
- Thực hiện kiểm tra lại giới hạn `maxScore` của các tiêu chí cập nhật.
- Cập nhật ghi đè các bản ghi cũ trong bảng `Scores` và `ScoreItems` trong cùng một transaction (BR-SCO-06).
- **`scores` array behavior**: Mảng `scores` ghi đè TOÀN BỘ `ScoreItems` cũ, không merge. Các item cũ bị soft-disable và thay thế bằng item mới.
- **`totalScore` validation**: Server kiểm tra tổng `scores[].score` khớp với `totalScore`. Nếu không khớp, trả `400 SCORE_TOTAL_MISMATCH`.
- API này cho phép Judge cập nhật lại điểm của chính mình theo schema hiện tại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "Bảng điểm này do giám khảo khác chấm, bạn không được sửa.",
  "messageCode": "SCORE_NOT_OWNED_BY_JUDGE",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | SCORE_LIMIT_EXCEEDED | Điểm cập nhật vượt quá điểm tối đa của rubric. |
| 400 | SCORE_TOTAL_MISMATCH | Tổng điểm chi tiết không khớp với totalScore gửi lên. |
| 403 | SCORE_NOT_OWNED_BY_JUDGE | Bảng điểm này do giám khảo khác chấm, bạn không được sửa. |
| 404 | SCORE_NOT_FOUND | Bảng điểm không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `PATCH /api/v1/judge/scores/{scoreId}`.
- Sử dụng policy `LecturerPolicy`.
