# Student nộp bài cho round

## Tác dụng
Student thuộc team đã được duyệt có thể nộp bài cho một round đang mở thời gian submission.

## URL
`POST /api/v1/rounds/{roundId}/submit-assignment`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id của round cần nộp bài. |

## Query parameters
Không có.

## Ví dụ request
```http
POST /api/v1/rounds/00000000-0000-0000-0000-000000000000/submit-assignment
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "url": "https://example.com/submission",
    "description": "Mô tả bài nộp"
}
```

## Request body
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `url` | `string` | Có | Đường dẫn bài nộp. |
| `description` | `string` | Không | Mô tả thêm cho bài nộp. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": {
    "submissionId": "00000000-0000-0000-0000-000000000000",
    "teamId": "00000000-0000-0000-0000-000000000000",
    "url": "https://example.com/submission",
    "submittedAt": "datetime"
  },
  "message": "SUBMISSION_CREATED_SUCCESSFULLY"
}
```

## Business rules
- `roundId` không được rỗng.
- `url` là bắt buộc và không được để trống.
- Round phải tồn tại và chưa bị disable (`IsDisable = false`).
- Nếu round có `StartSubmission`, thời điểm hiện tại phải lớn hơn hoặc bằng `StartSubmission` (Lỗi: `SUBMISSION_NOT_STARTED`).
- Nếu round có `EndSubmission`, thời điểm hiện tại phải nhỏ hơn hoặc bằng `EndSubmission` (Lỗi: `SUBMISSION_CLOSED`).
- Người gọi API phải thuộc team active của register team đã được duyệt (`Status = Approved`).
- Register team không được bị disable hoặc bị banned.
- Round detail tương ứng không được bị disable.
- Team chưa nộp bài lần nào trong round này (chỉ được submit 1 lần duy nhất). Nếu đã nộp trước đó sẽ trả về lỗi `ALREADY_SUBMITTED`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ROUND_ID_REQUIRED, URL_REQUIRED, SUBMISSION_NOT_STARTED, SUBMISSION_CLOSED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | USER_TEAM_NOT_ALLOWED_TO_SUBMIT_THIS_ROUND |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 409 | CONFLICT | ALREADY_SUBMITTED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
