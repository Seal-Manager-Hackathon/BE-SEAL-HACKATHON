# Tìm kiếm Track hệ thống (Search Tracks)

## Tác dụng
Lấy danh sách track, hỗ trợ tìm kiếm, lọc và phân trang toàn hệ thống.

## URL
`GET /api/v1/tracks`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Query Parameters:**
    *   `eventId` (Guid, Không bắt buộc): Lọc các track thuộc một event cụ thể.
    *   `keyword` (string, Không bắt buộc): Tìm kiếm không phân biệt hoa thường theo tên hoặc mô tả track.
    *   `isDisable` (bool, Không bắt buộc, mặc định: false): `true` để lấy cả track đã disable, `false` chỉ lấy track đang hoạt động.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng track trên một trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "Title": "Bảng A - Web Application",
        "description": "Phát triển Web.",
        "maxTeam": 50,
        "isDisable": false,
        "createdAt": "2026-06-21T08:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Nếu truyền `eventId`, event đó phải tồn tại và đang hoạt động, chỉ lọc các track thuộc event này.
- Kết quả được sắp xếp tăng dần theo `Title` của Track.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện chỉ định.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Các tham số truy vấn sai định dạng. |
| 404 | EVENT_NOT_FOUND | Event lọc không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
