# Admin tạo giải thưởng (Admin Create Award)

## Tác dụng
Cho phép Admin tạo một hạng mục giải thưởng mới trong event.

## URL
`POST /api/v1/admin/events/{eventId}/awards`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

## Request body
```json
{
  "name": "Giải Nhất",
  "description": "Đội thi xuất sắc nhất toàn giải.",
  "levelAward": 1,
  "numberOfAward": 1,
  "prize": 10000000
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `name` | `string` | Có | Tên giải thưởng. |
| `description` | `string` | Không | Mô tả chi tiết giải thưởng. |
| `levelAward` | `int` | Có | Thứ hạng giải thưởng (1: Nhất, 2: Nhì, 3: Ba, ...). |
| `numberOfAward` | `int` | Có | Số lượng giải cho hạng mục này. |
| `prize` | `decimal` | Có | Giá trị giải thưởng (VND). |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 201,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "AWARD_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- `name` là bắt buộc, không được để trống.
- `levelAward` xác định thứ hạng giải thưởng (1: Nhất, 2: Nhì, 3: Ba, 4: Khuyến khích, v.v.).
- `numberOfAward` xác định số lượng giải cho hạng mục này (mặc định 1).
- `prize` là giá trị giải thưởng (số dương, đơn vị VND).
- Khi tạo mới, bản ghi mặc định có `IsDisable = false`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | AWARD_NAME_REQUIRED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `Awards`.
