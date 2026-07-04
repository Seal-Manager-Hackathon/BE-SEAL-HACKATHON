# GET /api/v1/admin/events/{eventId}/staff/available

**Role:** Admin
**Policy:** AdminPolicy

## Mô tả

Lấy danh sách staff có sẵn để phân công vào event (chưa được assign, đang Active, ko bị disable).

## Request

### Route Parameters

| Param | Type | Mô tả |
|-------|------|-------|
| eventId | Guid | ID của event |

### Query Parameters

| Param | Type | Default | Mô tả |
|-------|------|---------|-------|
| keyword | string | null | Tìm kiếm theo tên hoặc email |
| pageIndex | int | 1 | Trang hiện tại |
| pageSize | int | 10 | Số bản ghi mỗi trang (max 100) |

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | Thành công |

### Body

```json
{
  "data": {
    "items": [
      {
        "id": "guid",
        "firstName": "Nguyễn",
        "lastName": "Văn A",
        "fullName": "Nguyễn Văn A",
        "email": "staff@example.com",
        "phoneNumber": "0123456789",
        "avatarUrl": ""
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 3
  }
}
```

## Logic

- Lọc User có Role == Staff && IsDisable == false && Status == Active
- Loại trừ staff đã được assign vào event (trong AssignEvents.EventId == eventId && !IsDisable)
- Có keyword search: FirstName + LastName hoặc Email
- Phân trang
