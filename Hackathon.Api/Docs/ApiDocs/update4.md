# Update 4 — Các thay đổi trong phiên git này (03/07/2026)

---

## 1. PATCH /api/v1/users/profile — UpdateProfileRequest

**Thay đổi:** Thêm `ImgUrl` (string), `LinkUrl` (string).

**Trước:**
```
firstName, lastName, phoneNumber, avatarUrl (file),
bio, address, dateOfBirth,
studentId, college
```

**Sau:**
```
firstName, lastName, phoneNumber, avatarUrl (file),
bio, address, dateOfBirth,
studentId, college,
imgUrl, linkUrl       ← mới
```

---

## 2. GET /api/v1/judge/tracks/{trackId}/submissions

**Thay đổi:** Đổi param `?status=` → `?isGraded=`

**Trước:** `?roundId=...&status=pending|graded|all`

**Sau:** `?roundId=...&isGraded=true|false`

| isGraded | Kết quả |
|----------|---------|
| Ko truyền | Tất cả |
| `true` | Đã chấm |
| `false` | Chưa chấm |

---

## 3. GET /api/v1/admin/users

**Thay đổi:** Thêm query param `keyword`.

**Trước:** `?role=&pageIndex=&pageSize=`

**Sau:** `?role=&keyword=&pageIndex=&pageSize=`
