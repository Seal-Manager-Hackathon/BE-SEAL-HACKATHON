# Admin cập nhật thông tin vòng thi (Admin Update Round)

## Tác dụng
Admin cập nhật thông tin của một vòng thi (Round). Chỉ cập nhật các field được gửi lên (partial update / PATCH).

## URL
`PATCH /api/v1/admin/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi. |

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

Tất cả các field đều là **không bắt buộc** — chỉ những field được gửi lên mới được cập nhật.

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Không | Tên vòng thi. |
| `description` | `string` | Không | Mô tả vòng thi. |
| `roundNo` | `int` | Không | Số thứ tự vòng. |
| `startTime` | `datetime` | Không | Thời gian bắt đầu vòng thi. |
| `endTime` | `datetime` | Không | Thời gian kết thúc vòng thi. |
| `startSubmission` | `datetime` | Không | Thời gian bắt đầu nộp bài. |
| `endSubmission` | `datetime` | Không | Thời gian kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Số lượng team tối đa cho vòng. |

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
- Round phải tồn tại, không bị soft-disable.
- Nếu gửi `name` thì không được để trống.
- Các field không gửi sẽ giữ nguyên giá trị cũ.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.RoundsController`.
- Route: `PATCH /api/v1/admin/rounds/{roundId}`.
- Sử dụng policy `AdminPolicy`.
- Message: `ROUND_UPDATED_SUCCESSFULLY`.
