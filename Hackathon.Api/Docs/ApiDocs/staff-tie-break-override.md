# Staff tie-break override

## Tác dụng
Staff can thiệp dữ liệu thủ công để quyết định đội đi tiếp khi xảy ra trường hợp bằng điểm nhau ở ranh giới cắt loại của Vòng thi.

## URL
`POST /api/staff/rounds/{roundId}/tie-break`

## Authorization
Yêu cầu access token hợp lệ và role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id vòng thi cần xử lý tie-break. |

## Request body
```json
{
  "advancedTeamIds": ["guid"],
  "eliminatedTeamIds": ["guid"],
  "reason": "string"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "roundId": "guid",
    "advancedTeamIds": ["guid"],
    "eliminatedTeamIds": ["guid"],
    "reason": "string",
    "message": "TIE_BREAK_OVERRIDE_APPLIED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Staff/Admin được thực hiện tie-break override.
- Round phải tồn tại và chưa bị soft-disable.
- Các team được truyền phải đang thuộc round qua `RoundDetails`.
- Chỉ dùng khi có trường hợp bằng điểm ở ranh giới cắt loại.
- `reason` là bắt buộc để ghi nhận lý do can thiệp thủ công.
- Không được đưa cùng một team vào cả `advancedTeamIds` và `eliminatedTeamIds`.
- Service cập nhật trạng thái đi tiếp/dừng lại trên dữ liệu round participation theo business rule hiện tại.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_OR_ADMIN_REQUIRED |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND_IN_ROUND |
| 400 | BAD_REQUEST | TIE_BREAK_REASON_REQUIRED |
| 400 | BAD_REQUEST | TIE_BREAK_TEAMS_REQUIRED |
| 400 | BAD_REQUEST | TEAM_CANNOT_BE_ADVANCED_AND_ELIMINATED |
| 409 | CONFLICT | TIE_BREAK_NOT_REQUIRED |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
