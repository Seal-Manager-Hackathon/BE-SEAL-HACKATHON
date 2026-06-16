# Judge get submissions

## Tác dụng
Giám khảo lấy danh sách bài thi thực tế thuộc Bảng đấu mình được giao để chấm điểm.

## URL
`GET /api/judge/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ và role `Lecturer` với event role `Judge`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id vòng thi cần lấy danh sách bài nộp. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo bảng đấu cụ thể nếu judge được assign nhiều track. |
| `status` | `string` | Không | Lọc theo trạng thái bài nộp. Giá trị theo `SubmissionStatusEnum`. |

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": [
    {
      "submissionId": "guid",
      "roundDetailId": "guid",
      "roundId": "guid",
      "teamId": "guid",
      "teamName": "string",
      "trackId": "guid",
      "trackName": "string",
      "topicId": "guid|null",
      "topicName": "string|null",
      "sourceUrl": "string|null",
      "demoUrl": "string|null",
      "fileUrl": "string|null",
      "status": "Submitted",
      "submittedAt": "datetimeoffset",
      "hasGraded": false
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại phải là Judge được assign vào event/track tương ứng.
- Chỉ trả submission thuộc track mà judge được phân công trong `AssignTracks`.
- Chỉ lấy submission thuộc round được truyền trên URL.
- Submission bị soft-disable không được trả về.
- Nếu có nhiều submission của cùng team trong một round, có thể trả latest submission hợp lệ theo rule chấm điểm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | JUDGE_REQUIRED |
| 403 | FORBIDDEN | JUDGE_NOT_ASSIGNED_TO_TRACK |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 400 | BAD_REQUEST | INVALID_QUERY_PARAMETER |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
