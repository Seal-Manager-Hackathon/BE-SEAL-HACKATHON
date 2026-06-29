# Staff/Admin kết thúc vòng đấu (End Round)

## Tác dụng
Kết thúc vòng đấu hiện tại và chuyển các team có điểm cao nhất (dựa trên giới hạn số team của vòng tiếp theo) sang vòng tiếp theo.

## URL
`POST /api/v1/rounds/{roundId}/end`

## Authorization
Yêu cầu access token hợp lệ với policy `StaffOrAdminPolicy` (role `Staff` hoặc `Admin`).

- Nếu là **Staff**: phải được phân công vào event chứa round đó (`AssignEvents`), nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu là **Admin**: không cần kiểm tra phân công.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id của round (vòng đấu) hiện tại cần kết thúc. |

## Query parameters
Không có.

## Ví dụ request
```http
POST /api/v1/rounds/9cb15a44-1234-4562-a3fc-3d963f66bfb9/end
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": {
    "closedRoundId": "9cb15a44-1234-4562-a3fc-3d963f66bfb9",
    "closedRoundName": "Idea Submission",
    "nextRoundId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "nextRoundName": "Final Demo",
    "nextRoundLimitTeam": 10,
    "totalTeamsAdvanced": 5,
    "advancedTeams": [
      {
        "rank": 1,
        "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Seed Innovators",
        "averageScore": 85.5,
        "latestSubmissionId": "33000000-0000-0000-0000-000000000001"
      },
      {
        "rank": 2,
        "teamId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "teamName": "Green Coders",
        "averageScore": 72.0,
        "latestSubmissionId": "33000000-0000-0000-0000-000000000003"
      }
    ]
  },
  "message": "ROUND_ENDED_SUCCESSFULLY | FINAL_ROUND_CLOSED_HACKATHON_ENDED"
}
```

## Business rules
- Người gọi API phải có role `Staff` hoặc `Admin`.
- Nếu là Staff: phải được phân công vào event chứa round đó.
- Round được gọi phải tồn tại và chưa bị disable (`IsDisable = false`).
- **Thời gian**: chỉ có thể kết thúc round nếu `EndTime` của round đã qua (thời gian hiện tại > round.EndTime). Nếu chưa, trả lỗi `ROUND_NOT_ENDED_YET` với mã 400.
- Tính điểm:
  - Mỗi team chỉ lấy **submission mới nhất** (dựa trên `SubmittedAt` / `CreatedAt`).
  - Lấy **trung bình cộng `TotalScore`** của tất cả judge (Scores) trên submission đó, **bỏ qua các Score có `IsMock = true`**.
  - Team không có bài nộp hoặc không có Score nào hợp lệ sẽ không được đi tiếp.
- Số lượng team đi tiếp được quyết định bởi trường `LimitTeam` của round tiếp theo (round có cùng `EventId` và `RoundNo = current RoundNo + 1`).
- Các team được xếp hạng giảm dần theo `AverageScore`, nếu điểm bằng nhau thì xếp theo tên team.
- Nếu team đã có `RoundDetail` cho round tiếp theo (trường hợp EndRound chạy lần 2), sẽ không tạo trùng.
- Round hiện tại sau đó sẽ bị đóng (được set `IsDisable = true`).
- Nếu không có vòng tiếp theo, thông báo trả về sẽ là `FINAL_ROUND_CLOSED_HACKATHON_ENDED` và `nextRoundId` sẽ là `null`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ROUND_NOT_ENDED_YET |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | User does not have the required permissions. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
