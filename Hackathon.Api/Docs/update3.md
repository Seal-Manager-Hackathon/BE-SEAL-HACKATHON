# Update 3 — Danh sách API thay đổi response (03/07/2026)

---

## 1. GET /api/v1/submissions/{submissionId}

**Thay đổi:** Thêm `registerTeamId` và `eventId` vào response.

**Lý do:** FE cần `registerTeamId` để gọi các API liên quan (submit, score), và `eventId` để điều hướng.

**Trước:**
```json
{
  "submissionId": "...",
  "roundDetailId": "...",
  "roundId": "...",
  "roundName": "...",
  "teamId": "...",
  "teamName": "...",
  "url": "..."
}
```

**Sau:**
```json
{
  "submissionId": "...",
  "roundDetailId": "...",
  "roundId": "...",
  "roundName": "...",
  "teamId": "...",
  "teamName": "...",
  "registerTeamId": "guid",   // ← mới
  "eventId": "guid",           // ← mới
  "url": "..."
}
```

---

## 2. GET /api/v1/rounds?eventId=

**Thay đổi:** Thêm `isEnded` vào response.

**Lý do:** FE cần biết round đã kết thúc hay chưa mà không phải tự so sánh thời gian. `isEnded = endTime < now`.

**Trước:**
```json
{ "isDisable": false }
```

**Sau:**
```json
{
  "isDisable": false,
  "isEnded": false   // ← mới
}
```

---

## 3. GET /api/v1/rounds/{roundId}

**Thay đổi:** Thêm `isEnded` vào response. (Giống #2)

**Trước:**
```json
{ "isDisable": false }
```

**Sau:**
```json
{
  "isDisable": false,
  "isEnded": false   // ← mới
}
```

---

## 4. GET /api/v1/rounds/teams/{teamId}

**Thay đổi:** Thêm `isEnded` vào response. (Giống #2)

**Trước:**
```json
{ "endSubmission": "datetime|null" }
```

**Sau:**
```json
{
  "endSubmission": "datetime|null",
  "isEnded": false   // ← mới
}
```

---

## 5. GET /api/v1/events/{eventId}/leaderboard

**Thay đổi:** Thêm `roundScores` vào response.

**Lý do:** FE muốn hiển thị điểm từng round của mỗi team, không chỉ tổng.

**Trước:**
```json
{
  "rank": 1,
  "teamId": "guid",
  "teamName": "...",
  "totalScore": 85,
  "levelAward": 1
}
```

**Sau:**
```json
{
  "rank": 1,
  "teamId": "guid",
  "teamName": "...",
  "totalScore": 85,
  "levelAward": 1,
  "roundScores": [     // ← mới
    { "roundId": "guid", "roundName": "Vòng 1", "roundNo": 1, "averageScore": 80 },
    { "roundId": "guid", "roundName": "Vòng 2", "roundNo": 2, "averageScore": 90 }
  ]
}
```

---

## 6. GET /api/v1/admin/users — Thêm query param keyword

**Thay đổi:** Thêm param `keyword` để lọc user theo tên.

**Lý do:** Admin muốn gõ tên để tìm user nhanh hơn.

**Trước:** `?role=&pageIndex=&pageSize=`

**Sau:** `?role=&keyword=&pageIndex=&pageSize=`

(không đổi response)

---

## 7. PATCH /api/v1/users/profile — Đổi request từ Form sang JSON

**Thay đổi:** 
- Request từ `multipart/form-data` → `application/json`
- Xoá `avatarUrl` khỏi API này

**Lý do:** Avatar có API riêng `PATCH /api/v1/users/avatar`. Gửi JSON nhẹ hơn, không cần FormData.

**Trước:**
```
POST multipart/form-data
  firstName, lastName, phoneNumber,
  avatarUrl (file),
  bio, address, dateOfBirth,
  studentId, college
```

**Sau:**
```json
{
  "firstName": "...",
  "lastName": "...",
  "phoneNumber": "...",
  "bio": "...",
  "address": "...",
  "dateOfBirth": "2000-01-15",
  "studentId": "...",
  "college": "..."
}
```

(không đổi response)

---

## 8. GET /api/v1/tracks/my-assignment — API mới

**Thay đổi:** API hoàn toàn mới, thay thế `GET /api/v1/judge/tracks` và `GET /api/v1/mentor/tracks`.

**Lý do:** Gom 2 API riêng thành 1 API chung, thêm param `role` để lọc.

**Endpoint:** `GET /api/v1/tracks/my-assignment?eventId={eventId}&role=Judge`

| Param | Kiểu | Bắt buộc | Mô tả |
|-------|------|:--------:|-------|
| `eventId` | Guid | ✓ | ID của event |
| `role` | string | ✗ | `Judge`, `Mentor`, hoặc bỏ qua (lấy role đầu) |

**Response:**
```json
{
  "data": {
    "assignEventId": "guid",
    "eventId": "guid",
    "eventName": "...",
    "role": "Judge",
    "tracks": [
      {
        "assignTrackId": "guid",
        "trackId": "guid",
        "trackTitle": "...",
        "trackDescription": "..."
      }
    ]
  }
}
```

---

## 9. Endpoint cũ đã xoá

| Route | Trạng thái | Thay thế |
|-------|-----------|----------|
| `GET /api/v1/judge/tracks` | **⛔ Đã xoá** | `GET /api/v1/tracks/my-assignment?role=Judge` |
| `GET /api/v1/mentor/tracks` | **⛔ Đã xoá** | `GET /api/v1/tracks/my-assignment?role=Mentor` |

**Lý do:** API mới gộp chung, linh hoạt hơn.

---

## Tổng hợp

| STT | API | Dạng thay đổi |
|-----|-----|--------------|
| 1 | `GET /api/v1/submissions/{id}` | Response thêm `registerTeamId`, `eventId` |
| 2 | `GET /api/v1/rounds?eventId=` | Response thêm `isEnded` |
| 3 | `GET /api/v1/rounds/{id}` | Response thêm `isEnded` |
| 4 | `GET /api/v1/rounds/teams/{teamId}` | Response thêm `isEnded` |
| 5 | `GET /api/v1/events/{id}/leaderboard` | Response thêm `roundScores` |
| 6 | `GET /api/v1/admin/users` | Thêm query param `keyword` |
| 7 | `PATCH /api/v1/users/profile` | Request: Form → JSON, bỏ avatar |
| 8 | `GET /api/v1/tracks/my-assignment` | ✨ API mới |
| 9 | `GET /api/v1/judge/tracks` | ⛔ Đã xoá |
| 10 | `GET /api/v1/mentor/tracks` | ⛔ Đã xoá |
