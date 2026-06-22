# API 57: Kết thúc vòng thi (Staff End Round)

## Tác dụng
Staff/Admin kết thúc vòng thi đấu hiện tại, khóa bài nộp, chốt sổ điểm của round và tự động đưa top team sang round kế tiếp theo giới hạn `LimitTeam` của round sau.

## URL
`POST /api/v1/rounds/{roundId}/end`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng thi đấu cần kết thúc.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa kết quả.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "totalAdvancedTeams": 10,
    "message": "ROUND_ENDED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Vòng thi đấu phải tồn tại trong DB, không bị soft-disable.
- Staff thực hiện phải có quyền quản lý sự kiện tương ứng (`AssignEvents` có UserId và EventId). Admin có quyền chạy trực tiếp.
- API thực hiện kết thúc vòng, hệ thống tự động khóa cổng nhận bài thi (đánh dấu hạn nộp bài kết thúc) và chốt sổ điểm của round hiện tại.
- Hệ thống tính điểm trung bình cuối cùng từ các giám khảo chấm thi cho từng team trong round; điểm sau khi chốt được dùng làm cơ sở xếp hạng round.
- Nếu còn round kế tiếp trong cùng event (`RoundNo` hiện tại + 1), hệ thống lấy `LimitTeam` của round kế tiếp làm số lượng team được vào vòng sau.
- Ví dụ: Round 2 có `limitTeam = 5` thì khi kết thúc Round 1, hệ thống chọn 5 team có điểm cao nhất ở Round 1 để tạo `RoundDetails` cho Round 2.
- Nếu không còn round kế tiếp, API chỉ chốt sổ round hiện tại và không tạo thêm `RoundDetails` mới.
- Việc chốt điểm, chọn top team và tạo `RoundDetails` vòng sau phải chạy trong cùng một **Database Transaction** để tránh lệch trạng thái.
- Cập nhật `UpdatedAt` của Round.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công quản lý vòng thi này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công phụ trách event thi đấu này (check BR-ASG-01). |
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh khi kết thúc vòng đấu. |
