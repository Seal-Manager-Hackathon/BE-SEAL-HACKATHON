# Staff/Admin get reports list

## Tác dụng
Staff/Admin xem danh sách tất cả các báo cáo/khiếu nại trong hệ thống, hỗ trợ lọc theo trạng thái, loại report và event.

## URL
`GET /api/v1/staff/reports`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Query parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `status` | `string` | Không | Lọc theo trạng thái: `Open`, `Closed`, `Approved`. Mặc định lấy tất cả. |
| `typeReport` | `string` | Không | Lọc theo loại report: `Phúc khảo`, `Lỗi hệ thống`,... |
| `eventId` | `guid` | Không | Lọc theo sự kiện. |
| `keyword` | `string` | Không | Tìm kiếm theo tiêu đề. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`, tối đa `100`). |

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "teamName": "Chiến binh công nghệ",
        "eventName": "SEAL Hackathon 2026",
        "title": "Yêu cầu phúc khảo bài nộp Vòng loại",
        "typeReport": "Phúc khảo",
        "status": 0,
        "statusName": "Open",
        "createdAt": "2026-06-22T08:00:00Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 5,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Staff/Admin phải đăng nhập bằng access token hợp lệ.
- Nếu người gọi là Staff: chỉ trả về report thuộc event mình được phân công (`AssignEvents`). Report ngoài phạm vi không xuất hiện trong danh sách.
- Nếu người gọi là Admin: không cần kiểm tra phân công.
- Sắp xếp mặc định theo `CreatedAt` giảm dần.
- Report bị soft-delete (`IsDisable = true`) không được trả về.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
