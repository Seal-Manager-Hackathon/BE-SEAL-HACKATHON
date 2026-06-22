# API 17: Lấy danh sách Track của Event (Event Tracks)

## Tác dụng
Lấy danh sách các track (bảng thi đấu) thuộc về một sự kiện (event) cụ thể, có hỗ trợ tìm kiếm, lọc trạng thái và phân trang.

## URL
`GET /api/v1/events/{eventId}/tracks`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): Id của event cần lấy danh sách track.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Từ khóa tìm kiếm theo `Title` hoặc `Description`.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định lấy `false`.
    *   `pageIndex` (int, Không bắt buộc, mặc định `1`): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định `10`): Số item mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "id": "guid",
        "eventId": "guid",
        "Title": "string",
        "description": "string|null",
        "maxTeam": 0,
        "isDisable": false,
        "createdAt": "datetimeoffset"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 0,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": null,
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Không yêu cầu auth, endpoint public.
- `eventId` là bắt buộc trên path. Nếu truyền `eventId`, event phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
