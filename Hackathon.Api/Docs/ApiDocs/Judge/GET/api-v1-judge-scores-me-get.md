# Judge xem danh sách điểm đã chấm (Judge My Scores)

## Tác dụng
Giúp Judge xem danh sách chi tiết các bài thi mà mình đã chấm điểm trong một event, bao gồm thông tin team, track, điểm số. Hỗ trợ lọc theo track, trạng thái chấm và phân trang.

## URL
`GET /api/v1/judge/scores/me`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên với vai trò Judge.

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |
| `trackId` | `guid` | Không | Lọc theo track cụ thể. |
| `isGraded` | `bool` | Không | `true`: bài đã chấm, `false`: bài chưa chấm (mặc định: `false`). |
| `pageIndex` | `int` | Không | Số trang hiện tại (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách điểm (phân trang).*
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
        "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "trackId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "trackTitle": "Bảng A - Web Application",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "totalScore": 85.5,
        "isRetake": false,
        "isMock": false,
        "submittedAt": "2026-06-22T08:00:00Z",
        "updatedAt": "2026-06-22T10:00:00Z"
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
- Người gọi phải là giảng viên có vai trò Judge đang hoạt động.
- Chỉ trả về các bảng điểm thật (không mock, không disable) của judge hiện tại.
- `isGraded` mặc định `false`: trả bài chưa chấm. Truyền `true` để lấy bài đã chấm.
- **Thay đổi logic:** Giờ chỉ trả về 1 score cuối mỗi team (group theo RegisterTeamId), không trả tất cả lịch sử chấm.
- Kết quả được sắp xếp theo thời gian cập nhật giảm dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Route: `GET /api/v1/judge/scores/me`.
- Sử dụng policy `LecturerPolicy`.
