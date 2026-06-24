# Phân công Mentor/Judge vào Track (Assign Track)

## Tác dụng
Cho phép Admin/Staff gán giảng viên đã có vai trò Mentor hoặc Judge trong event vào một bảng đấu (Track) cụ thể. Phân công theo track quyết định phạm vi trách nhiệm: Mentor phụ trách các team thuộc track đó; Judge chỉ xem và chấm các team/submission thuộc track đó.

## URL
`POST /api/v1/admin/assign-events/{id}/tracks`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `id` | `guid` | Có | ID phân công sự kiện của giảng viên. |

## Request body
```json
{
  "trackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "TRACK_ASSIGNED"
}
```

## Business rules
- Bản ghi `AssignEvents` và `Tracks` liên đới phải tồn tại trong DB, không bị soft-disable.
- Track gán phải thuộc cùng Event mà giảng viên đã được phân công.
- Tạo bản ghi mới trong bảng `AssignTracks`. Nếu giảng viên đã được gán vào track này rồi, báo conflict `TRACK_ALREADY_ASSIGNED`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ASSIGNMENT_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 409 | CONFLICT | TRACK_ALREADY_ASSIGNED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại.
