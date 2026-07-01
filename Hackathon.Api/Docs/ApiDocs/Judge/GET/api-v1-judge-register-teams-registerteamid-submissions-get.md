# Judge xem bài nộp mới nhất của team trong từng round

## Tác dụng
Judge xem bài nộp MỚI NHẤT của một team (theo `registerTeamId`) trong mỗi round.

**Mỗi round chỉ trả về 1 bài nộp duy nhất — bài cuối cùng của team trong round đó.**  
Các phiên bản cũ của team không được hiển thị cho Judge.

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
  "data": {
    "items": [
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
        "gradingStatus": "Graded",
        "scoreId": "guid|null",
        "totalScore": "decimal|null"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 5,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

*Mỗi round chỉ có 1 item — bài mới nhất.*

### Ý nghĩa các trường
| Trường | Ý nghĩa |
|--------|---------|
| `submissionId` | Id bài nộp |
| `roundId` | Id của round |
| `roundName` | Tên round |
| `roundNo` | Số thứ tự round |
| `roundDetailId` | Id của bản ghi RoundDetails |
| `url` | Link bài nộp |
| `description` | Mô tả bài nộp |
| `status` | Trạng thái bài nộp (0: Submitted, 1: Unsubmitted, 2: Failed) |
| `submittedAt` | Thời gian nộp bài |
| `gradingStatus` | `"Graded"` nếu judge đã chấm, `"Pending"` nếu chưa |
| `scoreId` | Id điểm số (null nếu chưa chấm) |
| `totalScore` | Tổng điểm (null nếu chưa chấm) |

## Business rules
- Yêu cầu access token hợp lệ với role `Lecturer` và phải là Judge trong event.
- Judge chỉ xem được team thuộc track mình được phân công (`AssignTracks`).
- Team phải có trạng thái `Approved` và không bị ban.
- **Chỉ lấy bài nộp mới nhất** của mỗi round detail (`.GroupBy().Select(g => g.OrderByDescending().First())`).
- Sắp xếp theo `SubmittedAt` giảm dần.
- Hỗ trợ phân trang.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/register-teams/{registerTeamId:guid}/submissions`.
- Policy: `LecturerPolicy`.
