# Lấy tóm tắt thống kê sự kiện (Event Summary Statistics)

## Tác dụng
Lấy nhanh tóm tắt thống kê của sự kiện (tổng số lượng đội thi được duyệt, số bảng đấu, số vòng thi) để vẽ giao diện dashboard của sự kiện.

## URL
`GET /api/v1/events/{eventId}/summary`

## Authorization
Public (không yêu cầu access token).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần lấy tóm tắt. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "totalApprovedTeams": 24,
    "totalTracks": 3,
    "totalRounds": 3,
    "totalAwards": 5
  },
  "message": "SUCCESS"
}
```

## Business rules
- Thống kê dữ liệu trực tiếp trong DB:
  - `totalApprovedTeams`: Đếm số team có `RegisterTeams.Status = Approved` và `IsBanned = false` trong event này.
  - `totalTracks`: Đếm số `Tracks` hoạt động của event.
  - `totalRounds`: Đếm số `Rounds` hoạt động của event.
  - `totalAwards`: Đếm số hạng mục giải thưởng của event.
- Event phải tồn tại trong DB, không bị soft-disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_ID |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `RegisterTeams`, `Tracks`, `Rounds`, `Awards`.
