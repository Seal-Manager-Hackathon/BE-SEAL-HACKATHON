# Staff/Admin kết thúc vòng đấu (End Round)

## Tác dụng
Kết thúc vòng đấu hiện tại và chuyển các team có điểm cao nhất (dựa trên giới hạn số team của vòng tiếp theo) sang vòng tiếp theo.

**API này là fallback cho trường hợp background job (EndRoundJob) bị lỗi.**  
Bình thường job sẽ tự động close round khi hết giờ, nhưng staff/admin có thể ấn thủ công để kích hoạt ngay lập tức.

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
    "roundId": "9cb15a44-1234-4562-a3fc-3d963f66bfb9",
    "roundName": "Idea Submission",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "nextRoundId": "4fb25a44-1234-4562-a3fc-3d963f66bfb9",
    "nextRoundName": "Final Demo",
    "nextRoundLimitTeam": 10,
    "totalTeams": 8,
    "totalAdvanced": 5,
    "teams": [
      {
        "rank": 1,
        "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Seed Innovators",
        "averageScore": 85.5,
        "latestSubmissionId": "33000000-0000-0000-0000-000000000001",
        "isAdvanced": true
      },
      {
        "rank": 2,
        "teamId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "teamName": "Green Coders",
        "averageScore": 72.0,
        "latestSubmissionId": "33000000-0000-0000-0000-000000000003",
        "isAdvanced": true
      },
      {
        "rank": 3,
        "teamId": "3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f",
        "teamName": "AI Mavericks",
        "averageScore": 60.0,
        "latestSubmissionId": "33000000-0000-0000-0000-000000000010",
        "isAdvanced": false
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
- **API này ghi dữ liệu (write)**:
  - Đóng round hiện tại: set `IsDisable = true`.
  - Chuyển top N team có điểm cao nhất (theo `LimitTeam` của round tiếp theo) sang round sau: tạo `RoundDetails` mới.
  - Nếu team đã có `RoundDetail` cho round tiếp theo, sẽ không tạo trùng.
- Tính điểm:
  - Mỗi team chỉ lấy **submission mới nhất** (dựa trên `SubmittedAt` / `CreatedAt`).
  - Chỉ lấy submissions có `Status = Submitted`, không lấy draft/unsubmitted.
  - Mỗi judge (AssignTrackId) chỉ lấy score mới nhất (tránh retake).
  - Lấy **trung bình cộng `TotalScore`** của các judge, **bỏ qua các Score có `IsMock = true`**.
- Filter:
  - Team phải có `RegisterTeam.Status = Approved`.
  - Team không bị ban (`IsBanned = false`).
  - Team và register team không bị disable.
- Xếp hạng: giảm dần theo `AverageScore`, nếu điểm bằng nhau thì xếp theo tên team.
- `isAdvanced = true` nếu team có điểm > 0 và nằm trong top `LimitTeam` của round sau.
- Nếu không có vòng tiếp theo, tất cả team đều `isAdvanced = false` và message trả về `FINAL_ROUND_CLOSED_HACKATHON_ENDED`.
- `nextRoundId` sẽ là `null` nếu đây là round cuối cùng.

## Background job (EndRoundJob)
- Khi admin tạo event, `EndRoundJob` tự động bắt đầu monitor event đó.
- Job kiểm tra mỗi 5 phút.
- Khi phát hiện round có `EndTime` đã qua, nó tự động close round + chuyển team (giống logic API này).
- Job dừng monitor event khi đã quá `event.EndTime`.
- Nếu job bị lỗi, staff/admin có thể ấn API này để chạy thủ công.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ROUND_NOT_ENDED_YET |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | User does not have the required permissions. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
