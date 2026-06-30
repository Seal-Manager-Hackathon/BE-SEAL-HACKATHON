# Judge xem danh sách team đã nộp bài trong round

## Tác dụng
Judge xem danh sách các team thuộc track được phân công đã nộp bài trong một round cụ thể của event. Hỗ trợ lọc theo track và trạng thái chấm điểm. Mỗi team chỉ trả về bài nộp mới nhất.

## URL
`GET /api/v1/judge/events/{eventId}/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event qua `AssignEvents` + `AssignTracks`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event. |
| `roundId` | `guid` | Có | Id của round cần xem danh sách team. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo track. Không truyền = lấy tất cả track judge được phân công. |
| `status` | `string` | Không | Lọc theo trạng thái chấm: `all` (tất cả), `pending` (chưa chấm đủ), `graded` (đã chấm đủ). Mặc định: `all`. |
| `pageIndex` | `int` | Không | Trang hiện tại. Mặc định: 1. |
| `pageSize` | `int` | Không | Số lượng item mỗi trang. Mặc định: 10. |

## Ví dụ request
```http
GET /api/v1/judge/events/00000000-0000-0000-0000-000000000000/rounds/11111111-1111-1111-1111-111111111111?trackId=22222222-2222-2222-2222-222222222222&status=pending&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination` — phân trang.

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
      "registerTeamId": "guid",
      "teamId": "guid",
      "teamName": "string",
      "trackId": "guid",
      "trackTitle": "string",
      "topicId": "guid|null",
      "topicTitle": "string|null",
      "submissionId": "guid|null",
      "submissionStatus": 0,
      "submittedAt": "datetime|null",
      "gradingStatus": "string",
      "totalScore": "decimal|null"
    }
  ],
  "pageIndex": 1,
  "pageSize": 10,
  "totalCount": 50,
  "message": "SUCCESS"
}
```

### Ý nghĩa các trường
| Trường | Ý nghĩa |
|--------|---------|
| `registerTeamId` | Id của đơn đăng ký (đội) trong event |
| `teamId` | Id của team |
| `teamName` | Tên team |
| `trackId` | Id track team đó thuộc về |
| `trackTitle` | Tên track |
| `topicId` | Id chủ đề team chọn (nếu có) |
| `topicTitle` | Tên chủ đề (nếu có) |
| `submissionId` | Id bài nộp mới nhất của team trong round |
| `submissionStatus` | Trạng thái bài nộp (0: Submitted, 1: Unsubmitted, 2: Failed) |
| `submittedAt` | Thời gian nộp bài |
| `gradingStatus` | `"Graded"` nếu judge đã chấm đủ tất cả criteria items, `"Pending"` nếu chưa |
| `totalScore` | Tổng điểm judge đã chấm cho bài nộp này (null nếu chưa chấm) |

## Business rules
- Yêu cầu access token hợp lệ với role `Lecturer` và phải là Judge trong event.
- Endpoint dùng policy `LecturerPolicy`.
- Judge chỉ được xem các team thuộc track mà mình được phân công (`AssignTracks`).
- Judge chỉ được xem các team có trạng thái `Approved` và không bị ban.
- Mỗi team chỉ trả về **1 bài nộp duy nhất** — bài nộp mới nhất theo `SubmittedAt`.
- `gradingStatus = "Graded"` nếu judge đã chấm tất cả criteria items trong template active của round, `"Pending"` nếu chưa chấm hoặc chấm thiếu.
- `trackId` query param lọc theo track cụ thể; null = lấy tất cả track judge được assign.
- `status` query param: `all` (không filter), `pending` (chỉ team chưa graded), `graded` (chỉ team đã graded).
- Sắp xếp: theo `SubmittedAt` giảm dần, cùng ngày thì theo tên team tăng dần.
- Hỗ trợ phân trang với `pageIndex` và `pageSize`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.JudgeController`.
- Method: `GetJudgeRoundTeams(Guid eventId, Guid roundId, Guid? trackId, string? status, PaginationRequest paginationRequest)`.
- Endpoint dùng route `GET /api/v1/judge/events/{eventId:guid}/rounds/{roundId:guid}` và `LecturerPolicy`.
