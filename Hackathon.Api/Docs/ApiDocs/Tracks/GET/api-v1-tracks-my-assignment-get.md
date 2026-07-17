# Xem phân công track của user hiện tại (Get My Track Assignment)

## Tác dụng
Xem phân công track của user hiện tại trong một event. Dùng cho Judge/Mentor muốn biết mình được gán vào track nào của event.

## URL
`GET /api/v1/tracks/my-assignment`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Không | ID của event cần kiểm tra phân công. |
| `role` | `string` | Không | Vai trò cần lọc (`Judge`, `Mentor`, `Staff`). |

## Ví dụ request
```http
GET /api/v1/tracks/my-assignment?eventId=3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": {
    "eventId": "guid",
    "eventName": "string",
    "role": "Judge",
    "tracks": [
      {
        "trackId": "guid",
        "trackName": "string"
      }
    ]
  },
  "message": "SUCCESS"
}
```

## Business rules
- Người gọi phải có token hợp lệ.
- Nếu không truyền `eventId`, trả về tất cả phân công của user.
- Nếu truyền `role`, lọc theo event role tương ứng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
