# Update 4 — Các thay đổi trong phiên git này (03/07/2026)

---

## 1. PATCH /api/v1/users/profile — UpdateProfileRequest

**Thay đổi:** Thêm `ImgUrl` (string), `LinkUrl` (string). Bỏ `AvatarUrl` (đã có endpoint riêng `PATCH /api/v1/users/me/avatar`). Endpoint nhận JSON (`application/json`), không dùng FormData nữa.

**Trước:**
```
firstName, lastName, phoneNumber, avatarUrl (file),
bio, address, dateOfBirth,
studentId, college
```

**Sau:**
```
firstName (string), lastName (string), phoneNumber (string),
bio (string?), address (string?), dateOfBirth (date?), studentId (string), college (string),
imgUrl (string?), linkUrl (string?)       ← mới, bỏ avatarUrl
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

---

## 4. GET /api/v1/judge/tracks/{trackId}/submissions — roundId nullable

**Thay đổi:** `roundId` từ `Guid` (bắt buộc) → `Guid?` (không bắt buộc).

**Trước:** `?roundId={guid}&isGraded=true&pageIndex=1&pageSize=10`

**Sau:** `?roundId={guid}&isGraded=true&pageIndex=1&pageSize=10` — nếu ko truyền `roundId`, lấy tất cả các round.

---

## 5. GET /api/v1/admin/events/{eventId} — Admin event detail (MỚI)

**Mô tả:** Endpoint riêng cho admin xem chi tiết event, không filter IsDisable hay Status.

| Endpoint | Auth | Ghi chú |
|---|---|---|
| `GET /api/v1/admin/events/{eventId}` | AdminPolicy | Xem được cả event đã xoá |
| `GET /api/v1/events/{eventId}` | Public | Chỉ xem event không disable, không Draft |

---

## 6. Tạo event — IsDisable = false

**Sửa:** Khi tạo event mới, `IsDisable` mặc định là `false` (không phải `true`). Event được ẩn bằng `Status = Draft`, không phải `IsDisable`.

