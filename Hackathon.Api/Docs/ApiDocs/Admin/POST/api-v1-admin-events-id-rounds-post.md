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
  "startTime": "2026-07-01T09:00:00+00:00",
  "endTime": "2026-07-03T18:00:00+00:00",
  "startSubmission": "2026-07-01T09:00:00+00:00",
  "endSubmission": "2026-07-03T12:00:00+00:00",
  "limitTeam": 20
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Có | Tên vòng thi. Không được rỗng. |
| `description` | `string` | Không | Mô tả chi tiết vòng thi. |
| `startTime` | `datetimeoffset` | Không | Thời gian bắt đầu vòng thi. |
| `endTime` | `datetimeoffset` | Không | Thời gian kết thúc vòng thi. |
| `startSubmission` | `datetimeoffset` | Không | Thời gian bắt đầu nộp bài. |
| `endSubmission` | `datetimeoffset` | Không | Thời gian kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Số team tối đa vòng này. Phải > 0 nếu truyền. |

**Không có field `roundNo`** — RoundNo được tự động gán = max RoundNo hiện tại + 1.

## Response body (Success - 201 Created)
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
- Event phải tồn tại, chưa disable và **chưa bắt đầu** (`StartTime > now`). Nếu event đã bắt đầu → 400 `EVENT_ALREADY_STARTED`.
- `name` là bắt buộc.
- `RoundNo` tự động = max RoundNo hiện tại trong event + 1 (bắt đầu từ 1).
- `NumberRound` của event tự động +1.
- Các validation time giữ nguyên.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 400 | BAD_REQUEST | EVENT_ALREADY_STARTED |
| 400 | BAD_REQUEST | INVALID_ROUND_TIME_RANGE |
| 400 | BAD_REQUEST | LIMIT_TEAM_MUST_BE_POSITIVE |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
