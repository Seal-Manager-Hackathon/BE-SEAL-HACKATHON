# API Reset — Tổng quan phân tách Role

> Ngày: 04/07/2026  
> Mục đích: Tách các API đang dùng chung cho nhiều role thành API riêng theo từng role

---

## Các Role thực tế trong hệ thống

| Role | Route prefix | Mô tả |
|------|-------------|-------|
| **Admin** | `/api/v1/admin/` | Full quyền — thấy mọi thứ, CRUD event/track/round/user |
| **Staff** | `/api/v1/staff/` | Quản lý event được assign — xử lý register-team, assign lecturer/judge |
| **Lecturer** | `/api/v1/lecturers/` | Chung cho Mentor + Judge — xem event/track/round được assign |
| **Judge** | `/api/v1/judge/` | Chỉ Judge — chấm điểm, xem submission, score |
| **Mentor** | `/api/v1/mentor/` | Chỉ Mentor — gửi notification, xem teams |
| **Student** | `/api/v1/...` (no prefix) | Sinh viên — đăng ký event, team, submission |

---

## Nguyên tắc tách

1. **Giữ nguyên API cũ** — FE đã map, không phá vỡ
2. **Thêm API mới** với prefix role để tách logic
3. **API không prefix → Student/public** — chỉ thấy `IsDisable=false`, `Status=Published|Closed`
4. **Admin API → prefix `/admin/`** — thấy mọi thứ, bỏ qua IsDisable
5. **Staff chỉ thấy event được assign** trong `AssignEvents`
6. **Lecturer/Mentor/Judge** chỉ thấy event/track được assign trong `AssignEvents` + `AssignTracks`

---

## Các API Student (không prefix) giữ nguyên

Các API hiện tại dùng chung sẽ giữ nguyên cho Student. Thêm bản role-specific tại prefix tương ứng.

---

## File structure

```
Docs/resetapi/
├── overview.md              ← file này
├── admin/                   ← Admin APIs
│   ├── GET/*.md
│   ├── POST/*.md
│   ├── PATCH/*.md
│   └── DELETE/*.md
├── staff/                   ← Staff APIs
├── lecturers/               ← Lecturer APIs (chung Mentor+Judge)
├── judge/                   ← Judge APIs
├── mentor/                  ← Mentor APIs
├── student/                 ← Student APIs (giữ nguyên)
├── shared/                  ← APIs dùng chung nhiều role
├── auth/                    ← Auth APIs
└── system/                  ← System APIs
```
