# Giảng viên tìm kiếm sự kiện được phân công

## Tác dụng
Giúp giảng viên (Lecturer) tìm kiếm các event mà mình được phân công với các bộ lọc: tên event, năm tổ chức, vai trò (Mentor/Judge).

## URL
`GET /api/v1/lecturers/events/search`

## Quyền
Lecturer đã được phân công trong sự kiện (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên event.
    *   `year` (int, Không bắt buộc): Lọc sự kiện theo năm tổ chức (ví dụ: `2026`).
    *   `eventRole` (int, Không bắt buộc): Lọc theo vai trò của giảng viên trong sự kiện (`0`: Mentor, `1`: Judge).
    *   `pageIndex` (int, Không bắt buộc, mặc định 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định 10): Số phần tử trên một trang (tối đa 100).

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` với định dạng camelCase:*
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
        "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "eventName": "SEAL Hackathon 2026",
        "season": "Mùa hè 2026",
        "startTime": "2026-07-01T08:00:00Z",
        "endTime": "2026-07-10T17:00:00Z",
        "role": 0, /* 0: Mentor, 1: Judge */
        "eventStatus": 1 /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
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
- Người gọi phải là giảng viên (`role = 3` tương ứng `RoleEnum.Lecturer` trong `Users`).
- Trích xuất thông tin phân công trong bảng nối `AssignEvents` liên kết với `EventRoles` của giảng viên hiện tại.
- **Chỉ lấy sự kiện không phải trạng thái `Draft`** (chỉ Published/Closed).
- Kết quả được sắp xếp theo thời gian bắt đầu sự kiện giảm dần, sau đó theo tên event tăng dần.
- Nếu không truyền bất kỳ bộ lọc nào, API này sẽ trả về tất cả danh sách sự kiện (giống `GET /api/v1/lecturers/events`).

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Sự kiện đang nháp, chưa công bố |
| `1` | Published | Sự kiện đã công bố và hoạt động |
| `2` | Closed | Sự kiện đã kết thúc và đóng lại |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO |
| 400 | BAD_REQUEST | PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_100 |
| 400 | BAD_REQUEST | INVALID_EVENT_ROLE |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
