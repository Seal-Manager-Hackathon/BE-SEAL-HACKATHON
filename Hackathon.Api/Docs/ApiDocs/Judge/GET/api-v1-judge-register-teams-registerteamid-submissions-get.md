# Judge xem lịch sử bài nộp của team

## Tác dụng
Judge xem tất cả bài nộp của một team (theo `registerTeamId`) trong event. Trả về danh sách bài nộp sắp xếp theo thời gian nộp giảm dần. Judge chỉ xem được nếu team thuộc track mà judge được phân công.

## URL
`GET /api/v1/judge/register-teams/{registerTeamId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event qua `AssignEvents` + `AssignTracks`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký (đội) cần xem bài nộp. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `pageIndex` | `int` | Không | Trang hiện tại. Mặc định: 1. |
| `pageSize` | `int` | Không | Số lượng item mỗi trang. Mặc định: 10. |

## Ví dụ request
```http
GET /api/v1/judge/register-teams/33333333-3333-3333-3333-333333333333/submissions?pageIndex=1&pageSize=10
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
      "submissionId": "guid",
      "roundId": "guid",
      "roundName": "string",
      "roundNo": 1,
      "roundDetailId": "guid",
      "url": "string|null",
      "description": "string|null",
      "status": 0,
      "submittedAt": "datetime|null",
      "gradingStatus": "string",
      "scoreId": "guid|null",
      "totalScore": "decimal|null"
    }
  ],
  "pageIndex": 1,
  "pageSize": 10,
  "totalCount": 5,
  "message": "SUCCESS"
}
```

### Ý nghĩa các trường
| Trường | Ý nghĩa |
|--------|---------|
| `submissionId` | Id bài nộp |
| `roundId` | Id của round bài nộp thuộc về |
| `roundName` | Tên round |
| `roundNo` | Số thứ tự round |
| `roundDetailId` | Id của bản ghi RoundDetails |
| `url` | Link bài nộp |
| `description` | Mô tả bài nộp |
| `status` | Trạng thái bài nộp (0: Submitted, 1: Unsubmitted, 2: Failed) |
| `submittedAt` | Thời gian nộp bài |
| `gradingStatus` | `"Graded"` nếu judge đã chấm bài này, `"Pending"` nếu chưa |
| `scoreId` | Id của điểm số judge đã chấm (null nếu chưa chấm) |
| `totalScore` | Tổng điểm judge đã chấm (null nếu chưa chấm) |

## Business rules
- Yêu cầu access token hợp lệ với role `Lecturer` và phải là Judge trong event.
- Endpoint dùng policy `LecturerPolicy`.
- Judge chỉ xem được team thuộc track mình được phân công (`AssignTracks`).
- Team phải có trạng thái `Approved` và không bị ban.
- `registerTeamId` là bắt buộc trên path.
- Trả về tất cả bài nộp của team trong tất cả round (không lọc round).
- Sắp xếp submissions theo `SubmittedAt` giảm dần (mới nhất đầu tiên).
- Hỗ trợ phân trang với `pageIndex` và `pageSize`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.JudgeController`.
- Method: `GetJudgeTeamSubmissions(Guid registerTeamId, PaginationRequest paginationRequest)`.
- Endpoint dùng route `GET /api/v1/judge/register-teams/{registerTeamId:guid}/submissions` và `LecturerPolicy`.
