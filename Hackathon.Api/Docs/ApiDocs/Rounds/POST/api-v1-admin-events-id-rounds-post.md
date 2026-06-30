# Admin tạo vòng thi trong event (Admin Create Round)

## Tác dụng
Admin tạo một vòng thi (Round) mới thuộc về một sự kiện (Event) cụ thể.

## URL
`POST /api/v1/admin/events/{eventId}/rounds`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

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
| `name` | `string` | Có | Tên vòng thi (vd: "Vòng Sơ loại", "Vòng Chung kết"). |
| `description` | `string` | Không | Mô tả chi tiết vòng thi. |
| `roundNo` | `int` | Không | Số thứ tự vòng trong event. |
| `startTime` | `datetime` | Không | Thời gian bắt đầu vòng thi. |
| `endTime` | `datetime` | Không | Thời gian kết thúc vòng thi. |
| `startSubmission` | `datetime` | Không | Thời gian bắt đầu cho phép nộp bài. |
| `endSubmission` | `datetime` | Không | Thời gian kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Số lượng team tối đa được tham gia vòng này (dùng cho thăng vòng). |

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
- Event phải tồn tại và chưa bị soft-disable. Nếu không, trả lỗi `404 Not Found` (`EVENT_NOT_FOUND`).
- `name` là bắt buộc, không được để trống hoặc chỉ chứa khoảng trắng.
- Round mới được tạo với `EventId = eventId` từ path.
- Nếu cung cấp `roundNo`, nên kiểm tra tính duy nhất trong cùng event (đề xuất: nếu trùng, trả `ROUND_NO_ALREADY_EXISTS`).
- Nếu cung cấp `startTime` và `endTime`: `startTime` phải <= `endTime`.
- Nếu cung cấp thời gian nộp bài (`startSubmission`/`endSubmission`):
  - `startSubmission` <= `endSubmission`.
  - Nếu cung cấp cả `startTime`/`endTime`, khoảng nộp bài phải nằm trong khoảng thời gian vòng thi.
- Nếu cung cấp `limitTeam`, giá trị phải > 0.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 400 | BAD_REQUEST | INVALID_ROUND_TIME_RANGE |
| 400 | BAD_REQUEST | INVALID_SUBMISSION_TIME_RANGE |
| 400 | BAD_REQUEST | LIMIT_TEAM_MUST_BE_POSITIVE |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | ROUND_NO_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Chưa implement trong code hiện tại.**
- Route đề xuất: `POST /api/v1/admin/events/{eventId}/rounds`.
- Entity: `Rounds`.
- DTO đề xuất: `CreateRoundRequest` (cần thêm mới).
- Đây là tài liệu API đề xuất để FE/BE thống nhất contract trước khi phát triển.
- Hiện tại chỉ có API `PATCH /api/v1/admin/rounds/{roundId}` để cập nhật round đã tồn tại.
