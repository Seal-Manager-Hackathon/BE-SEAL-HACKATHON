# Kiểm tra trạng thái thiết lập (Admin/Staff Setup Status)

## Tác dụng
Rà soát nhanh cấu hình của giải đấu để xem đã đủ điều kiện công bố (Publish) chưa (đã tạo ít nhất 1 round, gán rubric tiêu chí, bảng đấu, đề thi, giải thưởng, phân công nhân sự đầy đủ chưa).

## URL
`GET /api/v1/admin/events/{eventId}/setup-status`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff` phụ trách.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần rà soát. |

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
    "isReadyToPublish": false,
    "checks": {
      "hasRounds": true,
      "hasCriteria": false,
      "hasTracks": true,
      "hasTopics": true,
      "hasAwards": true,
      "hasAssignedStaff": true
    },
    "message": "NO_CRITERIA"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Kiểm tra tính tồn tại của các đối tượng liên quan:
  - Phải có ít nhất 1 `Rounds`.
  - Mọi `Rounds` phải được liên kết ít nhất 1 `CriteriaTemplates` có chứa `CriteriaItems`.
  - Phải cấu hình ít nhất 1 `Tracks` và trong đó có ít nhất 1 `Topics`.
  - Phải gán giải thưởng `Awards`.
  - Phải gán nhân sự vận hành `AssignEvents`.
- Nếu tất cả các điều kiện trên đều thỏa mãn, trả về `isReadyToPublish: true`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `GET /api/v1/admin/events/{eventId}/setup-status`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Kiểm tra 6 điều kiện: `Rounds`, `Criteria`, `Tracks`, `Topics`, `Awards`, `AssignEvents`.
