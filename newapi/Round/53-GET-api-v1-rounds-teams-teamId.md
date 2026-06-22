# API 53: Xem các vòng thi của đội (Get Team Rounds)

## Tác dụng
Lấy danh sách các vòng thi (Rounds) mà team hiện đang/đã tham gia thi đấu.

## URL
`GET /api/v1/rounds/teams/{teamId}`

## Quyền
Authenticated User (Yêu cầu đăng nhập, cho phép thành viên trong team xem)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.
*   **Query Parameters:**
    *   `eventId` (Guid, Không bắt buộc): Lọc theo event cụ thể.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách các vòng thi.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "roundName": "Vòng loại",
      "roundNo": 1,
      "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "isPassed": true
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Trả về danh sách các round mà team đã được gán thông qua bảng `RoundDetails` và đơn đăng ký `RegisterTeams` đã được duyệt.
- Sau khi team được BTC gán Track + Topic từ kết quả bốc thăm offline, hệ thống mặc định tạo `RoundDetails` cho round đầu tiên (`RoundNo = 1`).
- Khi Staff/Admin kết thúc một round, hệ thống chốt điểm và chỉ tạo `RoundDetails` cho round kế tiếp đối với top team theo `LimitTeam` của round kế tiếp.
- `isPassed` biểu thị việc team được đi tiếp vào vòng sau hay dừng chân (stopped).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy nhóm thi đấu tương ứng.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
