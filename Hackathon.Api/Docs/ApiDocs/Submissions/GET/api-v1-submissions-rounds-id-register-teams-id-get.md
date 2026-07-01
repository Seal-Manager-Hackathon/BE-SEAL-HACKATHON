# Get Round Team Submissions (Admin/Staff: all versions; Student/Judge: only latest)

## Tác dụng
Lấy danh sách bài nộp của một đội đăng ký trong một vòng thi cụ thể.

**Phân quyền xem:**
- **Admin, Staff:** Xem được TẤT CẢ các phiên bản nộp bài (lịch sử đầy đủ).
- **Student, Judge:** Chỉ xem được bài nộp MỚI NHẤT của team.

## URL
`GET /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}`

## Quyền
Yêu cầu đăng nhập. Kết quả trả về khác nhau tuỳ role:
- **Admin**: tất cả versions
- **Staff** (được phân công event): tất cả versions
- **Student** (thành viên team): chỉ bài mới nhất
- **Judge** (phân công track): chỉ bài mới nhất

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID vòng thi.
    *   `registerTeamId` (Guid, Bắt buộc): ID đơn đăng ký của đội thi.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định `1`)
    *   `pageSize` (int, Không bắt buộc, mặc định `10`, tối đa `100`)

## Ví dụ request
```http
GET /api/v1/submissions/rounds/2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e/register-teams/3fa85f64-5717-4562-b3fc-2c963f66afa6?pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
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
        "submittedAt": "2026-06-22T08:00:00Z",
        "isLatest": true
      },
      {
        "submissionId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
        "url": "https://github.com/seal-hackathon/team-project-web-v1",
        "description": "Bài thi lần 1.",
        "status": "Submitted",
        "submittedAt": "2026-06-20T10:00:00Z",
        "isLatest": false
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 2,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

*Nếu là Student hoặc Judge → `totalCount = 1`, luôn có `isLatest = true` (chỉ bài mới nhất).  
Nếu là Admin/Staff → `totalCount` = tổng số phiên bản, `isLatest` đánh dấu bài nào là cuối cùng.*

## Business rules
- `RoundDetails` phải tồn tại và chưa disable. Nếu không có → 404 `ROUND_DETAIL_NOT_FOUND`.
- **Admin, Staff:** Trả về tất cả submissions (phân trang đầy đủ), sort `submittedAt` giảm dần. Mỗi item có `isLatest` để biết bài cuối cùng.
- **Student (team member), Judge:** Chỉ trả về 1 submission — bài mới nhất (`.FirstOrDefaultAsync()` sort `submittedAt` desc). `isLatest` luôn `true`. Các phiên bản cũ bị ẩn.
- Danh sách được sắp xếp theo `submittedAt` giảm dần.
- Chỉ lấy các submission chưa bị disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO |
| 400 | BAD_REQUEST | PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_100 |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
