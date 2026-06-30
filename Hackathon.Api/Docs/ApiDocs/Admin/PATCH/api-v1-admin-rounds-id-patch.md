# Admin cập nhật thông tin vòng thi (Admin Update Round)

## Tác dụng
Admin cập nhật thông tin của một vòng thi (Round). Chỉ cập nhật các field được gửi lên (partial update / PATCH).

## URL
`PATCH /api/v1/admin/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.
Policy: `AdminPolicy`.

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
| `name` | `string` | Không | Tên vòng thi. Nếu truyền thì không được rỗng hoặc chỉ chứa khoảng trắng. |
| `description` | `string` | Không | Mô tả vòng thi. |
| `roundNo` | `int` | Không | Số thứ tự vòng. Nếu truyền thì nên > 0 và unique trong cùng event. |
| `startTime` | `datetimeoffset` | Không | Thời gian bắt đầu vòng thi. |
| `endTime` | `datetimeoffset` | Không | Thời gian kết thúc vòng thi. |
| `startSubmission` | `datetimeoffset` | Không | Thời gian bắt đầu nộp bài. |
| `endSubmission` | `datetimeoffset` | Không | Thời gian kết thúc nộp bài. |
| `limitTeam` | `int` | Không | Số lượng team tối đa cho vòng. Nếu truyền thì phải > 0. |

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
- Người gọi phải có role `Admin`.
- `roundId` phải là GUID hợp lệ trên path.
- Round phải tồn tại và chưa bị soft-disable. Nếu không, trả `404 Not Found` (`ROUND_NOT_FOUND`).
- Các field không gửi sẽ giữ nguyên giá trị cũ.
- Nếu gửi `name` thì sau khi trim không được rỗng.
- Nếu gửi `description` thì nên trim trước khi lưu.
- Nếu gửi `roundNo`, giá trị phải > 0.
- Nếu gửi `roundNo`, nên kiểm tra tính duy nhất trong cùng event với round chưa bị disable, trừ chính round đang update.
- Khi validate timeline, cần dùng giá trị sau update (merge request field + giá trị hiện tại trong DB).
- Nếu sau update có đủ `startTime` và `endTime`: `startTime` phải <= `endTime`.
- Nếu sau update chỉ có một trong hai field `startSubmission` / `endSubmission`, nên trả `SUBMISSION_TIME_RANGE_REQUIRED`.
- Nếu sau update có đủ `startSubmission` và `endSubmission`: `startSubmission` phải <= `endSubmission`.
- Nếu sau update có đủ round time + submission time: khoảng nộp bài phải nằm trong khoảng thời gian vòng thi.
- Nếu gửi `limitTeam`, giá trị phải > 0.
- Khi update thành công, set `UpdatedAt = DateTimeOffset.UtcNow`.

## Lỗi có thể xảy ra
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
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 409 | CONFLICT | ROUND_NO_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement đầy đủ** trong `Hackathon.Api.Controllers.AdminController`.
- Route hiện có: `PATCH /api/v1/admin/rounds/{roundId}`.
- Sử dụng policy `AdminPolicy` (attribute trên controller class).
- Service: `Hackathon.Service.Admin.Service.UpdateRound()`.
- DTO request: `CreateRoundRequest` (`AdminService.Request`, dùng chung với Create — tất cả field nullable để hỗ trợ partial update).
- Validation đầy đủ: roundNo > 0 + unique (trừ chính round đang update), timeline ranges, submission boundaries, name không rỗng nếu truyền, limitTeam > 0 nếu truyền.
- Partial update: merge request values + current DB values trước khi validate, chỉ cập nhật field được gửi lên.
