# Judge xem tất cả bài nộp trong round (không phân biệt track)

## Tác dụng
Giúp Judge xem danh sách tất cả các team + bài nộp mới nhất trong một round, **gồm tất cả track** mà judge được phân công.

Không cần trackId — API tự động lấy tất cả track của judge trong round đó.

## URL
`GET /api/v1/judge/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên được phân công Judge.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Có | ID của vòng cần xem. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `status` | `string` | Không | Lọc theo trạng thái chấm: `all` (tất cả, mặc định), `pending` (chưa chấm), `graded` (đã chấm). |
| `pageIndex` | `int` | Không | Số trang hiện tại (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách team + bài nộp (phân trang).*
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
        "trackId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
        "trackTitle": "Web Development",
        "registerTeamId": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "topicId": "d5e6f7a8-b9c0-1d2e-3f4a-5b6c7d8e9f0a",
        "topicTitle": "AI trong giáo dục",
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "submissionStatus": "Submitted",
        "submittedAt": "2026-06-22T08:00:00Z",
        "gradingStatus": "Pending",
        "scoreId": null,
        "totalScore": null
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 10,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

### Fields
| Tên | Kiểu | Mô tả |
|---|---|---|
| `trackId` | `guid` | ID của track. |
| `trackTitle` | `string` | Tên track. |
| `registerTeamId` | `guid` | ID bản ghi đăng ký team trong event. |
| `teamId` | `guid` | ID của team. |
| `teamName` | `string` | Tên team. |
| `topicId` | `guid?` | ID chủ đề team chọn (nếu có). |
| `topicTitle` | `string?` | Tên chủ đề. |
| `submissionId` | `guid?` | ID bài nộp mới nhất (null nếu chưa nộp). |
| `url` | `string?` | Link bài nộp. |
| `submissionStatus` | `string?` | Trạng thái bài nộp (`Submitted`, `Pending`...). |
| `submittedAt` | `datetime?` | Thời gian nộp bài. |
| `gradingStatus` | `string` | `NoSubmission` / `Pending` / `Graded`. |
| `scoreId` | `guid?` | ID điểm (null nếu chưa chấm). |
| `totalScore` | `decimal?` | Tổng điểm (null nếu chưa chấm). |

### Field `gradingStatus`
| Giá trị | Ý nghĩa |
|---|---|
| `NoSubmission` | Team chưa nộp bài cho round này. |
| `Pending` | Judge chưa chấm hoặc chấm chưa đủ hết tiêu chí (criteria items). |
| `Graded` | Judge đã chấm đủ số lượng tiêu chí theo template active của round. |

## Sắp xếp (Sort order)
- Các bài **chưa chấm** (`NoSubmission`, `Pending`) hiện **lên đầu**.
- Trong cùng nhóm, sắp xếp theo **SubmittedAt giảm dần** (bài mới nhất lên trước).
- Phân trang áp dụng **sau khi sắp xếp**, đếm theo số team.

## Business rules
- Giám khảo gọi API phải được phân công vào ít nhất 1 track của round đó. Nếu không có → trả về rỗng.
- Chỉ trả về các team thuộc track mà judge được phân công.
- Mỗi team chỉ xuất hiện **1 lần duy nhất** (lấy bài nộp mới nhất của team trong round đó).
- Nếu team chưa nộp bài → `submissionId = null`, `gradingStatus = "NoSubmission"`.
- `gradingStatus` so sánh **số lượng ScoreItem judge đã chấm** với tổng criteria items của template active.
- Support lọc theo `status`: `all` (mặc định), `pending` (gồm NoSubmission + Pending), `graded`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "FORBIDDEN",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/rounds/{roundId}/submissions`.
- Sử dụng policy `LecturerPolicy`.
