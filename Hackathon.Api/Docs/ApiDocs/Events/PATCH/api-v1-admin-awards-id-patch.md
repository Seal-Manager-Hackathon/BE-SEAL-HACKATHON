# Admin cập nhật giải thưởng (Admin Update Award)

## Tác dụng
Cho phép Admin cập nhật thông tin một hạng mục giải thưởng.

## URL
`PATCH /api/v1/admin/awards/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | ID của giải thưởng cần cập nhật. |

## Request body
API hỗ trợ partial update (chỉ cập nhật các trường được truyền khác null).
```json
{
  "name": "string",
  "description": "string|null",
  "levelAward": "string",
  "numberOfAward": 0,
  "prize": 0
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Không | Tên giải thưởng. |
| `description` | `string` | Không | Mô tả giải thưởng. |
| `levelAward` | `string` | Không | Cấp giải ("1", "2", ...). |
| `numberOfAward` | `int` | Không | Số lượng giải. |
| `prize` | `decimal` | Không | Giá trị giải thưởng (VND). |

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
  "message": "AWARD_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Bản ghi giải thưởng phải tồn tại và chưa bị soft-disable.
- Nếu `name` được truyền thì không được để trống.
- Cập nhật `UpdatedAt` của bản ghi.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 400 | BAD_REQUEST | AWARD_NAME_REQUIRED |
| 404 | NOT_FOUND | AWARD_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `UpdateAward(Guid id, UpdateAwardRequest request)` trong `Hackathon.Service.Events.IService`.
- Đã thêm request model `UpdateAwardRequest` trong `Hackathon.Service.Events.Request`.
- Đã implement logic partial update trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/awards/{id}` và `AdminPolicy`.
