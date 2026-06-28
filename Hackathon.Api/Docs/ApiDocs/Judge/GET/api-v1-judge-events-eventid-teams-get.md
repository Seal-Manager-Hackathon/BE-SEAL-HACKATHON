# Judge xem danh sách team cần chấm trong event

## Tác dụng
Judge (giảng viên được phân công vai trò Judge) xem danh sách các team thuộc các track được phân công chấm trong một event. Hỗ trợ lọc theo round. Mỗi team chỉ trả về bài nộp mới nhất (`latest submission`), kèm trạng thái đã chấm hay chưa.

## URL
`GET /api/v1/judge/events/{eventId}/teams`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event qua `AssignEvents` + `AssignTracks`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần xem danh sách team chấm. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Không | Lọc theo round. Không truyền = lấy tất cả round. |

## Ví dụ request
```http
GET /api/v1/judge/events/00000000-0000-0000-0000-000000000000/teams?roundId=11111111-1111-1111-1111-111111111111
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(data)` — trả về mảng `data` là danh sách track, mỗi track chứa danh sách team.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": [
    {
      "trackId": "guid",
      "trackTitle": "string",
      "teams": [
        {
          "registerTeamId": "guid",
          "teamId": "guid",
          "teamName": "string",
          "topicId": "guid|null",
          "topicTitle": "string|null",
          "submissionId": "guid|null",
          "submissionStatus": 0,
          "submittedAt": "datetime|null",
          "isGraded": false
        }
      ]
    }
  ],
  "message": "SUCCESS"
}
```

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thành công |
| `1` | Unsubmitted | Chưa nộp bài |
| `2` | Failed | Nộp bài thất bại |

## Business rules
- Yêu cầu access token hợp lệ với role `Lecturer` và phải là Judge trong event.
- Endpoint dùng policy `LecturerPolicy`.
- Judge chỉ được xem các team thuộc track mà mình được phân công (`AssignTracks`).
- Judge chỉ được xem các team có trạng thái `Approved` và không bị ban.
- Nếu truyền `roundId`: chỉ lấy team có bài nộp trong round đó.
- Nếu không truyền `roundId`: lấy team có bài nộp trong tất cả round của event.
- Mỗi team chỉ trả về **1 bài nộp duy nhất** — bài nộp có `SubmittedAt` lớn nhất (`latest submission`).
- `isGraded = true` nếu Judge đã chấm bài này (có `Scores` không mock, không disable).
- Sắp xếp: track theo tên, team trong track theo tên team tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.JudgeController`.
- Method: `GetJudgeTeamsByEvent(Guid eventId, Guid? roundId)`.
- Endpoint dùng route `GET /api/v1/judge/events/{eventId:guid}/teams` và `LecturerPolicy`.
