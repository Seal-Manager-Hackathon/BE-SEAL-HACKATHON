# Get Round Team Submissions

## Tác dụng
Lấy danh sách bài nộp của một đội đăng ký trong một vòng thi cụ thể. FE dùng API này để hiển thị lịch sử nộp bài theo round của team.

## URL
`GET /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}`

## Quyền
Yêu cầu đăng nhập. Team member của team, Staff/Admin có quyền trên event, hoặc Judge được phân công theo track có thể xem danh sách bài nộp.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID vòng thi cần xem bài nộp.
    *   `registerTeamId` (Guid, Bắt buộc): ID đơn đăng ký của đội thi.

*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định `1`): Trang cần lấy, phải lớn hơn 0.
    *   `pageSize` (int, Không bắt buộc, mặc định `10`): Số dòng mỗi trang, phải lớn hơn 0. Service giới hạn tối đa 100.

## Ví dụ request
```http
GET /api/v1/submissions/rounds/2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e/register-teams/3fa85f64-5717-4562-b3fc-2c963f66afa6?pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "Items": [
      {
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi hoàn chỉnh.",
        "status": "Submitted",
        "submittedAt": "2026-06-22T08:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- `RoundDetails` theo `roundId` và `registerTeamId` phải tồn tại và chưa bị disable. Nếu không tồn tại, trả lỗi `404 Not Found` với message `ROUND_DETAIL_NOT_FOUND`.
- Admin được xem.
- Staff được xem nếu được assign vào event của round.
- Team member được xem nếu user là thành viên active của team.
- Judge được xem nếu được assign vào track của register team trong cùng event.
- Danh sách bài nộp được sắp xếp theo `submittedAt` giảm dần.
- Chỉ lấy các submission chưa bị disable.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO |
| 400 | BAD_REQUEST | PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO |
| 400 | BAD_REQUEST | PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_100 |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
