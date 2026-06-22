# Kiểm tra trạng thái thiết lập (Admin/Staff Setup Status)

## Tác dụng
Rà soát nhanh cấu hình của giải đấu để xem đã đủ điều kiện công bố (Publish) chưa (đã tạo ít nhất 1 round, gán rubric tiêu chí, bảng đấu, đề thi, giải thưởng, phân công nhân sự đầy đủ chưa).

## URL
`GET /api/v1/admin/events/{eventId}/setup-status`

## Quyền
Admin hoặc Staff phụ trách (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần rà soát.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "isReadyToPublish": false,
    "checks": {
      "hasRounds": true,
      "hasCriteria": false,
      "hasTracks": true,
      "hasTopics": true,
      "hasAwards": true,
      "hasAssignedStaff": true
    },
    "message": "CRITERIA_NOT_FOUND_FOR_SOME_ROUNDS"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
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
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy event chỉ định.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Người gọi không có quyền quản lý sự kiện này (check BR-ASG-01). |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
