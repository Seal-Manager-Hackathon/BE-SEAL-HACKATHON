# Judge xem danh sách bài thi cần chấm

## Tác dụng
Giúp Judge xem danh sách các bài thi đã nộp của các team thuộc bảng đấu (Track) mình được phân công chấm điểm. Judge chỉ chấm các team thuộc track này, không chấm team ở track khác. Hỗ trợ phân trang.

## URL
`GET /api/v1/judge/tracks/{trackId}/submissions`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên được phân công Judge phụ trách track.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Có | ID của bảng đấu cần lấy bài thi. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `pageIndex` | `int` | Không | Số trang hiện tại (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách bài nộp (phân trang).*
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
        "isGraded": false,
        "scoreId": null,
        "totalScore": null
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Giám khảo gọi API phải được phân công chấm bảng đấu này (`AssignTracks` liên kết `TrackId` và `AssignEventId` của Judge). Nếu sai, từ chối xem và báo lỗi `FORBIDDEN`.
- Chỉ trả ra các bài nộp của các team thuộc đúng track mà judge được phân công.
- Judge chỉ được xem/chấm submission của team trong track được phân công; submission thuộc track khác phải bị từ chối.
- `isGraded` báo xem giám khảo hiện tại đã cho điểm bài thi này chưa.
- Hỗ trợ phân trang qua `pageIndex` và `pageSize`.

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
