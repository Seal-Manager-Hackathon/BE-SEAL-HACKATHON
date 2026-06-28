# Judge lọc bài nộp theo trạng thái chấm điểm (Judge Pending Submissions)

## Tác dụng
Giúp Judge lọc danh sách bài nộp theo trạng thái đã chấm hoặc chưa chấm trong một event. Hỗ trợ lọc theo track, round và phân trang.

## URL
`GET /api/v1/judge/events/{eventId}/submissions/pending`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo track cụ thể. |
| `roundId` | `guid` | Không | Lọc theo round cụ thể. |
| `isGraded` | `bool` | Không | `true`: bài đã chấm, `false`: bài chưa chấm (mặc định: `false`). |
| `pageIndex` | `int` | Không | Số trang (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` (phân trang).*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "registerTeamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "topicTitle": "Hệ thống quản lý y tế thông minh",
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "submissionStatus": 0,
        "submittedAt": "2026-06-22T08:00:00Z",
        "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "totalScore": 85.5
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

## Business rules
- `isGraded = true`: chỉ trả submission đã có điểm của judge này.
- `isGraded = false` (mặc định): chỉ trả submission chưa có điểm.
- Judge chỉ xem được submissions của các track được phân công.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Route: `GET /api/v1/judge/events/{eventId:guid}/submissions/pending`.
- Sử dụng policy `LecturerPolicy`.
