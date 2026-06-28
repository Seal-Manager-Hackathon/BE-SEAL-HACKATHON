# Nhật ký sửa đổi API — cho FE

---

## 1. Sửa route `POST /api/v1/staff/tracks/{trackId}/assign-lecturers`

**File:** `Hackathon.Api/Controllers/Staff.cs`

### Trước
```
POST /api/v1/staff/tracks/{trackId}/assign-lecturers
```
Không có eventId.

### Sau
```
POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers
```
Có eventId.

### Request body (không đổi)
```json
{ "assignEventId": "guid" }
```

### Response (không đổi)
```json
{ "id": "guid", "assignEventId": "guid", "trackId": "guid" }
```

### Sửa logic
- Thêm validate `track.EventId != eventId` → `TRACK_NOT_IN_EVENT`

---

## 2. Sửa response `GET /api/v1/staff/events/{eventId}/assignments`

### Trước
```json
{
  "items": [{
    "id": "guid", "userId": "guid", "firstName": "string", "lastName": "string",
    "email": "string", "eventRoleId": "guid", "eventRole": 1, "role": 3,
    "isDisable": false, "createdAt": "datetimeoffset"
  }]
}
```
Không có track info.

### Sau
```json
{
  "items": [{
    "id": "guid", "userId": "guid", "firstName": "string", "lastName": "string",
    "email": "string", "eventRoleId": "guid", "eventRole": 1, "role": 3,
    "isDisable": false, "createdAt": "datetimeoffset",
    "assignedTracks": [{
      "assignTrackId": "guid", "trackId": "guid", "trackTitle": "string", "isDisable": false
    }]
  }]
}
```
Thêm `assignedTracks` cho mỗi item.

### Query parameter mới
- `trackId` (guid?) — lọc những người được phân công vào track cụ thể

### Sửa logic
- **Admin:** Xem được Staff + Lecturer
- **Staff:** Chỉ xem được Lecturer (không thấy Staff khác)

---

## 3. Sửa `GET /api/v1/staff/events/{eventId}/lecturers/available`

**Request — xóa `EventRoleId` (Guid), thêm `userId` + `email`**

### Trước
```json
{
  "eventRoleId": "guid",     // bắt buộc
  "keyword": "string"
}
```

### Sau
```json
{
  "keyword": "string",       // Không bắt buộc
  "userId": "guid",          // Mới — search theo UserId
  "email": "string"          // Mới — search theo email
}
```

### Sửa logic
- **Trước:** Lookup EventRoles DB theo `EventRoleId`, validate Staff, filter conflict role Mentor/Judge
- **Sau:** Tự động loại tất cả lecturer đã có `AssignEvents` trong event (bất kỳ role nào). Thêm search bằng `userId` / `email`.

---

## 4. API mới: Admin Users

### `GET /api/v1/admin/users`
Lấy tất cả user, phân trang.

### `GET /api/v1/admin/users/search`
Tìm kiếm user với filter: `mailSearch`, `idSearch`, `role`, `studentIdSearch`, `isDisable`, `isVerified`.

---

## 5. API mới: Roles

### `GET /api/v1/roles`
Danh sách RoleEnum (Admin/Staff/Student/Lecturer). Không cần auth.

### `GET /api/v1/roles/event-roles`
Danh sách EventRoleEnum từ DB (Mentor/Judge/Staff). Không cần auth.

---

## 6. Sửa heading EventRoleEnum (14 file doc)
`### Bảng vai trò EventRoleEnum` → `### Bảng vai trò EventRoleEnum (Integer)`
