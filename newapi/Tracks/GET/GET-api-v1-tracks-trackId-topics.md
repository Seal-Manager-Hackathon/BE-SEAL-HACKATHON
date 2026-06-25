# API 50: Lấy danh sách đề bài theo Track (Track Topics)

## Tác dụng
Lấy danh sách các topic thuộc về một track cụ thể, có hỗ trợ tìm kiếm, lọc trạng thái và phân trang.

## URL
`GET /api/v1/tracks/{trackId}/topics`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): Id của track cần lấy danh sách topic.
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
        "trackId": "guid",
        "Title": "string",
        "description": "string|null",
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
- `trackId` là bắt buộc trên path. Nếu truyền `trackId`, track phải tồn tại và chưa bị soft-disable, nếu không trả `TRACK_NOT_FOUND`.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
