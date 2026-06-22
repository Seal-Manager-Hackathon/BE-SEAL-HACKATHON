# API 52: Lấy danh sách vòng thi (Get Rounds)

## Tác dụng
Lấy danh sách các vòng thi (Round) thuộc cấu hình của một sự kiện (event).

## URL
`GET /api/v1/rounds`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Query Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event để lấy danh sách round.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách các vòng thi.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "id": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "name": "Vòng loại",
      "description": "Nộp sản phẩm đề tài tự chọn.",
      "roundNo": 1,
      "startTime": "2026-07-01T08:00:00Z",
      "endTime": "2026-07-01T18:00:00Z",
      "startSubmission": "2026-07-01T08:00:00Z",
      "endSubmission": "2026-07-01T17:30:00Z",
      "limitTeam": 50,
      "isDisable": false
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- `eventId` là bắt buộc. Event tương ứng phải tồn tại và đang hoạt động (`IsDisable = false`), nếu không trả lỗi `EVENT_NOT_FOUND`.
- Trả ra danh sách các round chưa bị disable, sắp xếp theo thứ tự vòng thi đấu (`RoundNo` tăng dần).
- Team sau khi được gán Track + Topic từ kết quả bốc thăm offline sẽ mặc định bắt đầu ở round đầu tiên của event (`RoundNo = 1`).
- Các round sau (`RoundNo > 1`) chỉ có team khi Staff/Admin kết thúc round trước và hệ thống chọn top team theo `LimitTeam` của round kế tiếp.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện thi đấu chỉ định.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Thiếu eventId hoặc sai định dạng GUID. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại hoặc đã bị ẩn. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
