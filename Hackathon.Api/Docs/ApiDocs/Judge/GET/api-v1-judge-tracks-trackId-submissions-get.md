# Judge xem danh sách bài nộp của các team trong 1 round, 1 track

## Tác dụng
Giúp Judge xem danh sách các team đã nộp bài trong 1 round của 1 track mà judge được phân công.

**Chỉ lấy bài nộp MỚI NHẤT của mỗi team (theo từng round).**  
Bỏ qua các lần nộp cũ hơn của team — mỗi team chỉ xuất hiện 1 lần.

## URL
`GET /api/v1/judge/tracks/{trackId}/submissions?roundId={roundId}`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên được phân công Judge phụ trách track.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Có | ID của bảng đấu cần lấy bài thi. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Không | ID của vòng cần xem. Nếu **không truyền**, lấy tất cả các round của track đó. |
| `isGraded` | `bool` | Không | `true`: bài đã chấm, `false` hoặc ko truyền: bài chưa chấm (mặc định: `false`). |
| `pageIndex` | `int` | Không | Số trang hiện tại (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách team + bài nộp mới nhất (phân trang).*
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
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "roundName": "Vòng loại",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi hoàn thiện.",
        "status": "Submitted",
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
- Giám khảo gọi API phải được phân công chấm bảng đấu này. Nếu sai, từ chối xem và báo lỗi `FORBIDDEN`.
- **`roundId` là không bắt buộc** — nếu ko truyền, lấy bài nộp mới nhất của mỗi team ở **tất cả các round** trong track.
- Mỗi team có thể xuất hiện **nhiều lần** (1 lần mỗi round) khi ko truyền `roundId`.
- Nếu truyền `roundId`, mỗi team chỉ xuất hiện **1 lần duy nhất** (lấy bài nộp mới nhất của team trong round đó).
- Nếu team chưa nộp bài → `submissionId = null`, `gradingStatus = "NoSubmission"`, không có score.
- `gradingStatus` so sánh **số lượng ScoreItem judge đã chấm** với tổng criteria items của template active.
- Support phân trang qua `pageIndex` và `pageSize`, đếm theo số lượng **team** (không phải submission).

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
| 403 | FORBIDDEN | Không có quyền chấm bảng đấu này. |
| 404 | TRACK_NOT_FOUND | Bảng đấu không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/tracks/{trackId}/submissions`.
- Sử dụng policy `LecturerPolicy`.
