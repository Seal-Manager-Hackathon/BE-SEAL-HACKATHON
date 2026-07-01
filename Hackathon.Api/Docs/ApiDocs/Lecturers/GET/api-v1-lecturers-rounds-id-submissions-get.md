# Judge lấy danh sách bài nộp của vòng thi (Judge Get Round Submissions)

## Tác dụng
Judge lấy danh sách bài nộp (submissions) MỚI NHẤT của các team trong một vòng thi. 
Chỉ lấy được các team thuộc track mà judge được phân công.
Chỉ lấy được khi vòng thi đã đóng thời gian nộp bài (`EndSubmission`).

**Quan trọng:** Mỗi team chỉ xuất hiện **1 lần** — chỉ lấy bài nộp mới nhất.

## URL
`GET /api/v1/lecturers/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và được gán vai trò `Judge` trong event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định 1). |
| `pageSize` | `int` | Không | Số lượng bản ghi mỗi trang (mặc định 10). |

## Request body
Không có.

## Response body
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
        "submissionId": "guid|null",
        "roundDetailId": "guid",
        "teamId": "guid",
        "teamName": "string",
        "trackId": "guid|null",
        "trackTitle": "string|null",
        "topicId": "guid|null",
        "topicTitle": "string|null",
        "url": "string|null",
        "description": "string|null",
        "submissionStatus": "Submitted|null",
        "submittedAt": "datetime|null",
        "averageScore": 0.0
      }
    ],
    "pagination": {
      "pageIndex": 1,
      "pageSize": 10,
      "totalCount": 0,
      "totalPages": 0
    }
  }
}
```

## Business rules
- Judge phải được phân công vào event (`AssignEvents` với role `Judge`).
- Judge chỉ thấy được các team thuộc track mà họ được phân công (`AssignTracks`).
- Chỉ lấy submission mới nhất (`SubmittedAt` giảm dần) của mỗi team.
- Chỉ cho phép xem khi **vòng thi đã đóng nộp bài** (`EndSubmission` đã qua).
- Nếu vòng thi chưa đóng → lỗi `ROUND_SUBMISSION_STILL_OPEN`.
- Sắp xếp theo tên team.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_SUBMISSION_STILL_OPEN |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | JUDGE_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.LecturersController`.
- Route: `GET /api/v1/lecturers/rounds/{roundId}/submissions`.
- Sử dụng policy `LecturerPolicy`.
