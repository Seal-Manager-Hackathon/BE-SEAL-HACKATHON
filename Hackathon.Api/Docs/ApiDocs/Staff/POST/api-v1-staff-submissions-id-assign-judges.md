# Staff/Admin assign judges to submission

## Tác dụng
Staff/Admin phân công một hoặc nhiều Judge chấm điểm cho một bài nộp cụ thể. Giúp BTC chủ động chỉ định giám khảo phù hợp cho từng bài thi thay vì gán judge theo track toàn bộ.

## URL
`POST /api/v1/staff/submissions/{submissionId}/assign-judges`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `submissionId` | `guid` | Có | Id của bài nộp cần phân công judge. |

## Query parameters
Không có.

## Ví dụ request
```http
POST /api/v1/staff/submissions/8fa95f64-5717-4562-b3fc-2c963f66afa6/assign-judges
Authorization: Bearer {accessToken}
Content-Type: application/json
```

## Request body
```json
{
  "judgeIds": [
    "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
    "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e"
  ]
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `judgeIds` | `array<guid>` | Có | Danh sách ID của các judge được phân công chấm bài này. |

## Response body
Response dùng `ApiResponseFactory.Base(data)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": null,
  "timestampUtc": "datetime",
  "data": {
    "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
    "assignedJudges": [
      {
        "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
        "judgeName": "Nguyễn Văn A",
        "email": "nguyenvana@school.edu.vn",
        "hasScored": false,
        "totalScore": null,
        "isFinalized": false
      },
      {
        "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
        "judgeName": "Trần Thị B",
        "email": "tranthib@school.edu.vn",
        "hasScored": false,
        "totalScore": null,
        "isFinalized": false
      }
    ]
  },
  "message": "JUDGES_ASSIGNED_SUCCESSFULLY"
}
```

## Business rules
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này yêu cầu role `Staff` hoặc `Admin` qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `submissionId` là bắt buộc trên path.
- Submission phải tồn tại và chưa bị soft-disable, nếu không trả `SUBMISSION_NOT_FOUND`.
- Nếu người gọi là Staff: phải được phân công vào event chứa round của submission đó (`AssignEvents`) thì mới được phân công judge, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công.
- Tất cả `judgeIds` phải là user có role `Lecturer` và đã được gán role `Judge` trong event của submission này.
- Team của submission phải đã được gán track (`RegisterTeams.TrackId` không null), nếu không trả `TRACK_NOT_FOUND_FOR_SUBMISSION`.
- **Kiểm tra track assignment:** Mỗi judge trong `judgeIds` chỉ được phép chấm bài nộp của team thuộc track mà judge đó đã được phân công (`AssignTracks.TrackId`). Nếu judge chưa được gán vào track của bài nộp, trả lỗi `JUDGE_NOT_ASSIGNED_TO_TRACK`.
- Nếu một judge không tồn tại hoặc không có quyền Judge trong event, trả lỗi kèm `JUDGE_NOT_VALID`.
- Nếu trùng lặp judgeId, bỏ qua các bản ghi trùng (idempotent).
- API này **validate và trả về danh sách judge** hợp lệ dựa trên `AssignTracks` hiện có. Việc phân công judge được quản lý ở cấp track thông qua `AssignTracks`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | JUDGE_NOT_VALID |
| 400 | BAD_REQUEST | JUDGE_NOT_ASSIGNED_TO_TRACK |
| 400 | BAD_REQUEST | TRACK_NOT_FOUND_FOR_SUBMISSION |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
