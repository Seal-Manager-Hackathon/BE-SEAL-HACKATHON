# Kết thúc sự kiện (Admin Close Event)

## Tác dụng
Chuyển trạng thái sự kiện sang `Closed` (Đã đóng) sau khi kết thúc toàn bộ vòng thi và công bố bảng vàng.

## URL
`PATCH /api/v1/admin/events/{eventId}/close`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần đóng. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": null,
  "message": "EVENT_CLOSED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chuyển trạng thái `Status = Closed` (giá trị enum `2`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Kể từ thời điểm đóng sự kiện, toàn bộ dữ liệu điểm số, bảng xếp hạng và bài nộp thi của event chuyển sang trạng thái chỉ đọc (Read-only), không cho phép Judge cập nhật điểm hay thí sinh sửa bài nộp (BR-SCO-07, BR-LB-06).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Nháp (Thí sinh không nhìn thấy) |
| `1` | Published | Đang diễn ra / Mở đăng ký |
| `2` | Closed | Đã đóng / Kết thúc giải đấu |
| `3` | Cancelled | Đã hủy |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `Events.Status = Closed`.
