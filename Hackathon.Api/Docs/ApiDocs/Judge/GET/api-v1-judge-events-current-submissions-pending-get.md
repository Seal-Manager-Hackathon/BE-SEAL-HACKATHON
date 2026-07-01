# Judge xem bài nộp chưa chấm trong event đang diễn ra (Judge Current Event Pending)

## Tác dụng
Giúp Judge tự động tìm event đang diễn ra (dựa trên thời gian hiện tại) và lấy danh sách bài nộp chưa chấm trong event đó.

## URL
`GET /api/v1/judge/events/current/submissions/pending`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge`.

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo track cụ thể. |
| `roundId` | `guid` | Không | Lọc theo round cụ thể. |
| `pageIndex` | `int` | Không | Số trang (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử trên trang (mặc định: 10, tối đa: 100). |

## Response body (Success - 200 OK)
*Cấu trúc giống `BasePaginationResponse` như các API submissions khác.*

## Business rules
- **Mỗi team chỉ xuất hiện 1 lần trong mỗi round** — chỉ lấy bài nộp mới nhất (`.GroupBy().First()`).
- Tự động tìm event đang diễn ra (`StartTime ≤ now ≤ EndTime`).
- Chỉ trả bài chưa chấm (pending).
- Nếu không có event nào đang diễn ra, trả về danh sách rỗng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Route: `GET /api/v1/judge/events/current/submissions/pending`.
- Sử dụng policy `LecturerPolicy`.
