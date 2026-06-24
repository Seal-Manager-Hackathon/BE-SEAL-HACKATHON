# Hủy sự kiện (Admin Cancel Event)

## Tác dụng
Chuyển trạng thái sự kiện sang `Cancelled` (Đã hủy) khi giải đấu không thể tiếp tục tổ chức.

## URL
`PATCH /api/v1/admin/events/{eventId}/cancel`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần hủy. |

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
  "message": "EVENT_CANCELLED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chuyển trạng thái `Status = Cancelled` (giá trị enum `3`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Khi hủy sự kiện, đóng cổng nộp bài ở tất cả các vòng đấu liên kết.

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
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `Events.Status`.
