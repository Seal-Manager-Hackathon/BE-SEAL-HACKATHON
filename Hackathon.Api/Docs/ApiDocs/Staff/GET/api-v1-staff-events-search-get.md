# Staff tìm kiếm sự kiện được phân công

## URL
`GET /api/v1/staff/events/search`

## Quyền
Staff.

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên sự kiện.
    *   `startTime` (datetime, Không bắt buộc): Lọc sự kiện có thời gian bắt đầu >= giá trị này.
    *   `endTime` (datetime, Không bắt buộc): Lọc sự kiện có thời gian kết thúc <= giá trị này.
    *   `pageIndex` (int, Không bắt buộc, mặc định 1): Số trang.
    *   `pageSize` (int, Không bắt buộc, mặc định 10): Số item mỗi trang.

## Response body (Success - 200 OK)
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
        "assignEventId": "guid",
        "eventId": "guid",
        "eventName": "SEAL Hackathon 2026",
        "season": "Mùa hè 2026",
        "startTime": "2026-07-01T08:00:00Z",
        "endTime": "2026-07-10T17:00:00Z",
        "role": 1,
        "eventStatus": 1
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

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
