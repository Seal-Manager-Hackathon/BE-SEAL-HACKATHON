# Nhật ký sửa đổi API — cho FE

## 1. Sửa route `POST /api/v1/staff/tracks/{trackId}/assign-lecturers`

### Trước
`POST /api/v1/staff/tracks/{trackId}/assign-lecturers` ← không có eventId

### Sau
`POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers`

### Request body (không đổi)
```json
{
  "assignEventId": "guid"
}
```

### Response (không đổi)
```json
{
  "isSuccess": true,
  "status": 200,
  "data": { "id": "guid", "assignEventId": "guid", "trackId": "guid" },
  "message": "LECTURER_ASSIGNED_TO_TRACK_SUCCESSFULLY"
}
```

---

## 2. Sửa response `GET /api/v1/staff/events/{eventId}/assignments`

### Trước (thiếu assignedTracks, thiếu filter trackId)
```json
{
  "data": {
    "items": [
      {
        "id": "guid",
        "userId": "guid",
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "eventRoleId": "guid",
        "eventRole": 1,
        "role": 3,
        "isDisable": false,
        "createdAt": "datetimeoffset"
      }
    ]
  }
}
```

### Sau (thêm assignedTracks)
```json
{
  "data": {
    "items": [
      {
        "id": "guid",
        "userId": "guid",
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "eventRoleId": "guid",
        "eventRole": 1,
        "role": 3,
        "isDisable": false,
        "createdAt": "datetimeoffset",
        "assignedTracks": [
          {
            "assignTrackId": "guid",
            "trackId": "guid",
            "trackTitle": "string",
            "isDisable": false
          }
        ]
      }
    ]
  }
}
```

### Query parameter mới
| Param | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Không | Lọc những người được phân công vào track cụ thể |

### Sửa logic
- **Admin:** Xem được Staff + Lecturer
- **Staff:** Chỉ xem được Lecturer (không thấy Staff khác)

---

## 3. API mới: `DELETE /api/v1/staff/assign-tracks/{id}`
*(Tạo mới — không phải sửa, chỉ note cho FE)*

Xóa mềm lecturer khỏi track (giữ nguyên trong event).

---

## 4. API mới: `GET /api/v1/roles`
*(Tạo mới)*

Lấy danh sách RoleEnum hệ thống (Admin/Staff/Student/Lecturer). Không cần auth.

### Response
```json
{
  "data": [
    { "id": 0, "name": "Admin", "displayName": "Admin" },
    { "id": 1, "name": "Staff", "displayName": "Staff" },
    { "id": 2, "name": "Student", "displayName": "Student" },
    { "id": 3, "name": "Lecturer", "displayName": "Lecturer" }
  ]
}
```

---

## 5. API mới: `GET /api/v1/roles/event-roles`
*(Tạo mới)*

Lấy danh sách EventRoleEnum (Mentor/Judge/Staff) từ DB. Không cần auth.

### Response
```json
{
  "data": [
    { "id": 0, "name": "Mentor", "displayName": "Mentor" },
    { "id": 1, "name": "Judge", "displayName": "Judge" },
    { "id": 2, "name": "Staff", "displayName": "Staff" }
  ]
}
```
