# Admin cập nhật vòng thi (Admin Update Round)

## Tác dụng
Admin cập nhật thông tin một vòng thi. Partial update — chỉ cập nhật field gửi lên.

## URL
`PATCH /api/v1/admin/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi. |

## Request body
```json
{
  "name": "Vòng Chung Kết",
  "description": "Vòng thi cuối cùng",
  "roundNo": 2,
  "startTime": "2026-07-10T09:00:00+00:00",
  "endTime": "2026-07-12T18:00:00+00:00",
  "startSubmission": "2026-07-10T09:00:00+00:00",
  "endSubmission": "2026-07-12T12:00:00+00:00",
  "limitTeam": 10
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Không | Tên vòng thi. |
| `description` | `string` | Không | Mô tả. |
| `roundNo` | `int` | Không | Số thứ tự vòng — **hoán đổi** với round đang giữ số này. |
| `startTime` | `datetimeoffset` | Không | Thời gian bắt đầu. |
| `endTime` | `datetimeoffset` | Không | Thời gian kết thúc. |
| `startSubmission` | `datetimeoffset` | Không | Bắt đầu nộp bài. |
| `endSubmission` | `datetimeoffset` | Không | Kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Giới hạn team. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": null,
  "message": "ROUND_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Round phải tồn tại và chưa disable → 404 `ROUND_NOT_FOUND`.
- **Nếu event đã bắt đầu (`StartTime <= now`):** chỉ cho phép sửa `name`/`description`. Các field critical (`startTime`, `endTime`, `startSubmission`, `endSubmission`, `roundNo`, `limitTeam`) bị từ chối → 400 `EVENT_ALREADY_STARTED`.
- **`roundNo` hoán đổi (swap):** Truyền `roundNo` mới → tìm round khác trong cùng event đang giữ số đó → hoán đổi RoundNo giữa 2 round. Không phải ghi đè, không phải đánh lại số.
- Các field time không gửi giữ nguyên giá trị cũ.
- Validation time range giống create.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 400 | BAD_REQUEST | ROUND_NO_MUST_BE_POSITIVE |
| 400 | BAD_REQUEST | EVENT_ALREADY_STARTED |
| 400 | BAD_REQUEST | INVALID_ROUND_TIME_RANGE |
| 400 | BAD_REQUEST | LIMIT_TEAM_MUST_BE_POSITIVE |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | TARGET_ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
