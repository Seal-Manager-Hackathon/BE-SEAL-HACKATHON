# Admin tạo vòng thi trong event (Admin Create Round)

## Tác dụng
Admin tạo một vòng thi (Round) mới thuộc về một sự kiện (Event) cụ thể.

## URL
`POST /api/v1/admin/events/{eventId}/rounds`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.
Policy: `AdminPolicy`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện chứa vòng thi. |

## Request body
```json
{
  "name": "Vòng Sơ loại",
  "description": "Vòng thi đầu tiên của cuộc thi",
  "roundNo": 1,
  "startTime": "2026-07-01T09:00:00+00:00",
  "endTime": "2026-07-03T18:00:00+00:00",
  "startSubmission": "2026-07-01T09:00:00+00:00",
  "endSubmission": "2026-07-03T12:00:00+00:00",
  "limitTeam": 20
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Có | Tên vòng thi. Không được rỗng hoặc chỉ chứa khoảng trắng. |
| `description` | `string` | Không | Mô tả chi tiết vòng thi. |
| `roundNo` | `int` | Không | Số thứ tự vòng trong event. Nên > 0 và unique trong cùng event. |
| `startTime` | `datetimeoffset` | Không | Thời gian bắt đầu vòng thi. |
| `endTime` | `datetimeoffset` | Không | Thời gian kết thúc vòng thi. |
| `startSubmission` | `datetimeoffset` | Không | Thời gian bắt đầu cho phép nộp bài. |
| `endSubmission` | `datetimeoffset` | Không | Thời gian kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Số lượng team tối đa được tham gia vòng này (dùng cho thăng vòng). Phải > 0 nếu truyền. |

## Response body (Success - 201 Created)
*Cấu trúc trả về dạng `BaseResponse`:*

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 201,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "roundId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "ROUND_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải có role `Admin`.
- `eventId` phải là GUID hợp lệ trên path.
- Event phải tồn tại và chưa bị soft-disable. Nếu không, trả `404 Not Found` (`EVENT_NOT_FOUND`).
- Request body bắt buộc phải có.
- `name` là bắt buộc, sau khi trim không được rỗng.
- `name` nên được trim trước khi lưu.
- `description` nếu truyền nên được trim trước khi lưu.
- Round mới được tạo với `EventId = eventId` từ path, không nhận `eventId` từ body.
- Nếu cung cấp `roundNo`, giá trị phải > 0.
- Nếu cung cấp `roundNo`, nên kiểm tra tính duy nhất trong cùng event với round chưa bị disable. Nếu trùng, trả `ROUND_NO_ALREADY_EXISTS`.
- Nếu cung cấp cả `startTime` và `endTime`: `startTime` phải <= `endTime`.
- Nếu chỉ cung cấp một trong hai field `startSubmission` / `endSubmission`, nên trả `SUBMISSION_TIME_RANGE_REQUIRED` để tránh round không thể nộp bài.
- Nếu cung cấp cả `startSubmission` và `endSubmission`: `startSubmission` phải <= `endSubmission`.
- Nếu cung cấp đủ `startTime`, `endTime`, `startSubmission`, `endSubmission`: khoảng nộp bài phải nằm trong khoảng thời gian vòng thi.
- Nếu cung cấp `limitTeam`, giá trị phải > 0.
- Round tạo mới có `IsDisable = false`, `CreatedAt = now`, `UpdatedAt = now`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 400 | BAD_REQUEST | ROUND_NO_MUST_BE_POSITIVE |
| 400 | BAD_REQUEST | INVALID_ROUND_TIME_RANGE |
| 400 | BAD_REQUEST | SUBMISSION_TIME_RANGE_REQUIRED |
| 400 | BAD_REQUEST | INVALID_SUBMISSION_TIME_RANGE |
| 400 | BAD_REQUEST | SUBMISSION_TIME_OUTSIDE_ROUND_TIME |
| 400 | BAD_REQUEST | LIMIT_TEAM_MUST_BE_POSITIVE |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | ROUND_NO_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.AdminController`.
- Route hiện có: `POST /api/v1/admin/events/{eventId}/rounds`.
- Sử dụng policy `AdminPolicy` (attribute trên controller class).
- Service: `Hackathon.Service.Admin.Service.CreateRound()`.
- DTO request: `CreateRoundRequest` (`AdminService.Request`) — fields: name, description, roundNo, startTime, endTime, startSubmission, endSubmission, limitTeam.
- DTO response: `CreateRoundResponse` (`AdminService.Response`) — chứa `roundId`.
- Entity: `Rounds` — tạo mới với `IsDisable = false`, audit timestamps.
- Validation đầy đủ: name required, roundNo > 0 + unique trong event, timeline ranges, submission time boundaries, limitTeam > 0.
