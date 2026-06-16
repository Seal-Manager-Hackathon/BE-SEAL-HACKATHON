# Admin create topics

## Tác dụng
Admin tạo mới các Chủ đề (Topic) nằm trong một Bảng đấu (Track) cụ thể để Staff gán cho các đội sau này.

## URL
`POST /api/admin/tracks/{trackId}/topics`

## Authorization
Yêu cầu access token hợp lệ và role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Có | Id bảng đấu cần tạo topic. |

## Request body
```json
{
  "name": "string",
  "description": "string|null"
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
    "id": "guid",
    "trackId": "guid",
    "name": "string",
    "description": "string|null",
    "message": "TOPIC_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Admin được tạo topic.
- Track phải tồn tại và chưa bị soft-disable.
- Tên topic là bắt buộc.
- Tên topic không được trùng trong cùng một track.
- Topic là đề/chủ đề thi và được Staff gán cho team sau khi bốc thăm offline.
- Topic phải luôn thuộc một track cụ thể.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_REQUIRED |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 400 | BAD_REQUEST | TOPIC_NAME_REQUIRED |
| 409 | CONFLICT | TOPIC_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
