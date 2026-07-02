---
name: api-test
description: Use when the user wants to test API endpoints on the production server, verify correctness, check docs, audit code for bugs/500s, and test consistency across similar endpoints
---

# API Test Skill

## Connection Info

- **Server:** `https://api.hkathon.top`
- **Swagger JSON:** `https://api.hkathon.top/swagger/v1/swagger.json`
- **Swagger UI:** `https://api.hkathon.top/swagger/index.html` (403 without auth)
- **DB:** PostgreSQL at `dpg-d8of2nmrnols73d06k6g-a.singapore-postgres.render.com:5432`, database `seal_vatu`, user `admin`, pass `klSoev6KEkaIjDWapdj1iXfpNz4EVXEP`

## Test Accounts

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@hackathon.edu.vn | 123456Aa@ |
| Lecturer | lecturer1@school.edu.vn | 123456Aa@ |
| Student | student1@school.edu.vn | 123456Aa@ |

---

## Required Workflow — ALWAYS follow this order

### Phase 1: Code Review

Before any testing, read the service code and check:

#### 1.1 Null Safety
- `FirstOrDefaultAsync()` có null check + `NotFoundException` không?
- `?.` có được dùng toàn bộ LINQ chain không? (`?.Where()`, `?.Select()`, `?.FirstOrDefault()`)
- Navigation property có được `.Include()` đầy đủ trước khi access trong memory không?
- Nếu access trong `.Select()` projection → EF tự JOIN (an toàn)
- Nếu access sau `.ToListAsync()` trong memory → cần `.Include()` rõ ràng

#### 1.2 Pagination Safety
- `pageIndex <= 0 → 1`, `pageSize <= 0 → 10`, `pageSize > 100 → 100`
- `paginationRequest.PageIndex` / `PageSize` dùng trực tiếp không? → sanitize trước!
- Nếu `Request` class tự define `PageIndex`/`PageSize` thay vì kế thừa `PaginationRequest`? → sửa.

#### 1.3 Scoring & Formula
- Scoring có dùng `TotalScore` không? → phải dùng `ScoreItems` (avg per criteria → sum)
- Có filter `!IsMock` không?
- Có lấy latest Score per judge không? (GroupBy AssignTrackId, OrderByDesc UpdatedAt)
- Judge chấm lại (retake) không cộng dồn — chỉ lấy bản mới nhất

#### 1.4 Filter & Get Consistency
- GET list → filter `!IsDisable` không?
- Các API GET cùng role có cùng filter logic không? (ví dụ: Staff GET events đều filter `Status != Draft`)
- Sort order có đồng nhất không? (non-admin: StartTime desc, admin: CreatedAt desc)
- "Chỉ lấy bài mới nhất" — có GroupBy + OrderByDesc + First không?

#### 1.5 Authorization
- Endpoint có attribute `[Authorize]` / Policy phù hợp không?
- Trong Service, có check quyền cụ thể (ví dụ: judge chỉ xem track được phân công) không?
- Role enum check đúng không? (Staff, Admin, Lecturer, Student)
- Endpoint cho lecturer nhưng policy là `LecturerPolicy`? Judge dùng `LecturerPolicy`?

#### 1.6 Exception Handling — 500 Prevention
- `FirstOrDefaultAsync()` → nếu null → `throw NotFoundException()` (không để NullReferenceException)
- Navigation access trong memory → thiếu Include? → 500
- `.Where()` trên object null → 500 (dùng `?.Where()` hoặc null check)
- `.Average()`/`.Sum()` trên list rỗng → exception với int/decimal, an toàn với nullable
- `.Skip(-10)` khi pageIndex = 0 → ArgumentOutOfRangeException → 500 (phải sanitize)
- Chia cho 0 → không có (không dùng phép chia trực tiếp trong code)
- Parse enum → dùng `Enum.TryParse` (không dùng `Enum.Parse` không try-catch)

### Phase 2: Doc Check

- Doc mô tả đúng request/response không? (field names, types, optional/required)
- Business rules trong doc khớp với code không?
- Error codes table đủ các trường hợp không? (400, 401, 403, 404, 500)
- Nếu response thay đổi → doc đã update chưa?
- Nếu chỉ đổi logic (ko đổi response) → doc ghi chú scoring không?

### Phase 2.5: Temporary Auth Removal for Local Testing

Khi test **local** (localhost), chỉ test được public endpoint. Endpoint cần auth:

- **Muốn test local:** comment tạm `[Authorize]` / `[Authorize(Policy = ...)]` trong controller
- **Test xong PHẢI tự động thêm lại** y hệt cũ
- **Không tự ý xóa** — báo user push lên server nếu cần test auth API

Public endpoints (ko cần auth):
- `POST /api/v1/auth/login`
- `POST /api/v1/auth/register`
- `GET /api/v1/events`
- `GET /api/v1/events/{eventId}`
- `GET /api/v1/events/events/joined`
- `GET /api/v1/events/most-participants`
- `GET /api/v1/rounds/{roundId}/ranking`
- `GET /api/v1/enums`
- `GET /api/v1/tracks`
- `GET /api/v1/roles`
- `GET /api/v1/rounds/{roundId}/criteria`
- `GET /api/v1/events/{eventId}/criteria`

### Phase 3: Deploy (if code changed)

Nếu có code thay đổi chưa deploy:
1. Báo user: **"Cần push code lên server trước khi test — bạn commit + push không?"**
2. Chờ user xác nhận, chạy `git push origin develop`
3. Đợi ~30s server deploy xong
4. Sau đó mới test

### Phase 4: API Test on Server

```bash
# 1. Login
LOGIN=$(curl -s "https://api.hkathon.top/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@hackathon.edu.vn","password":"123456Aa@"}')
TOKEN=$(echo $LOGIN | python3 -c "import sys,json; print(json.load(sys.stdin)['data']['accessToken'])" 2>/dev/null || echo "LOGIN_FAILED")

# If login fails, try the known tokens from memory (api-test-server-info.md)

# 2. Call API
curl -s "https://api.hkathon.top/api/v1/..." \
  -H "Authorization: Bearer $TOKEN" \
  | python3 -m json.tool

# 3. Check response
# - HTTP 200 + isSuccess: true → ✅
# - isFailed: true → check message field
# - Empty items when expecting data → check filter logic
# - 401/403 → token expired or wrong role
```

### Phase 5: Report

Report format:
```
## API: <method> <path>

### Code Review
- [✅/❌] Null safety: ...
- [✅/❌] Pagination: ...
- [✅/❌] Scoring: ...
- [✅/❌] Auth: ...
- [✅/❌] 500 prevention: ...

### Doc Check
- [✅/❌] Response fields match: ...
- [✅/❌] Business rules: ...

### API Test
- [✅/❌] HTTP status: ...
- [✅/❌] Data correct: ...
- [✅/❌] Filter/Sort: ...

### Issues Found
- ...
```

## Endpoint Catalog (full list)

### Auth
| Method | Path |
|--------|------|
| POST | /api/v1/auth/login |
| POST | /api/v1/auth/register |
| GET | /api/v1/auth/me |
| POST | /api/v1/auth/logout |
| PATCH | /api/v1/auth/change-password |
| POST | /api/v1/auth/email-verifications |
| POST | /api/v1/auth/email-verifications/resend |
| POST | /api/v1/auth/forgot-password |
| POST | /api/v1/auth/reset-password |

### Admin
| Method | Path |
|--------|------|
| GET | /api/v1/admin/users |
| GET | /api/v1/admin/users/search |
| GET | /api/v1/admin/events/rounds |
| POST | /api/v1/admin/events/rounds |
| PATCH | /api/v1/admin/rounds |
| DELETE | /api/v1/admin/rounds |
| PATCH | /api/v1/admin/rounds/restore |
| POST | /api/v1/admin/events/rounds/criteria |
| GET | /api/v1/admin/events/rounds/criteria |
| PATCH | /api/v1/admin/events/rounds/criteria/activate |
| POST | /api/v1/admin/events |
| PATCH | /api/v1/admin/events |
| DELETE | /api/v1/admin/events |
| GET | /api/v1/admin/events/{eventId}/rounds |
| POST | /api/v1/admin/events/{eventId}/rounds |
| GET | /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria |
| POST | /api/v1/admin/notifications |
| PATCH | /api/v1/admin/users/role |

### Judge
| Method | Path |
|--------|------|
| GET | /api/v1/judge/tracks |
| GET | /api/v1/judge/tracks/{trackId}/submissions |
| GET | /api/v1/judge/events/{eventId}/submissions |
| GET | /api/v1/judge/events/{eventId}/submissions/pending |
| GET | /api/v1/judge/events/{eventId}/submissions/search |
| GET | /api/v1/judge/events/current/submissions/pending |
| GET | /api/v1/judge/submissions/{submissionId}/criteria |
| GET | /api/v1/judge/submissions/{submissionId}/scores/me |
| GET | /api/v1/judge/scores/me |
| GET | /api/v1/judge/submissions/regrade |
| GET | /api/v1/judge/rounds/{roundId}/submissions |
| GET | /api/v1/judge/register-teams/{registerTeamId}/submissions |
| GET | /api/v1/judge/events/{eventId}/rounds/{roundId} |
| GET | /api/v1/judge/events/{eventId}/teams |
| POST | /api/v1/judge/submissions/{submissionId}/scores |
| POST | /api/v1/judge/submissions/{submissionId}/scores/mock |
| PATCH | /api/v1/judge/scores/{scoreId} |
| POST | /api/v1/judge/scores/{scoreId}/finalize |
| POST | /api/v1/judge/scores/{scoreId}/retake |

### Staff
| Method | Path |
|--------|------|
| GET | /api/v1/staff/rounds/{roundId}/submissions |
| GET | /api/v1/staff/submissions/regrade |
| POST | /api/v1/staff/submissions/{submissionId}/assign-judges |
| POST | /api/v1/staff/assign-events |
| DELETE | /api/v1/staff/assign-events/{id} |
| POST | /api/v1/staff/assign-tracks |
| DELETE | /api/v1/staff/assign-tracks/{id} |

### Student / Team
| Method | Path |
|--------|------|
| POST | /api/v1/teams |
| PATCH | /api/v1/teams/{teamId} |
| GET | /api/v1/teams/{teamId} |
| DELETE | /api/v1/teams/{teamId}/members |
| POST | /api/v1/register-teams |
| GET | /api/v1/rounds/{roundId}/submissions |
| GET | /api/v1/rounds/{roundId}/my-submissions |
| GET | /api/v1/rounds/{roundId}/scores/me |
| GET | /api/v1/rounds/{roundId}/ranking |
| POST | /api/v1/rounds/{roundId}/submit-assignment |
| GET | /api/v1/submissions/{submissionId} |
| POST | /api/v1/events/{eventId}/invitations |

### Notifications
| Method | Path |
|--------|------|
| GET | /api/v1/notifications/me |
| GET | /api/v1/notifications/me/unread-count |
| PATCH | /api/v1/notifications/{notificationId}/read |
| PATCH | /api/v1/notifications/read-all |
| PATCH | /api/v1/notifications/all/disable |

### Public
| Method | Path |
|--------|------|
| GET | /api/v1/events |
| GET | /api/v1/events/{eventId} |
| GET | /api/v1/events/events/joined |
| GET | /api/v1/events/most-participants |
| GET | /api/v1/events/{eventId}/criteria |
| GET | /api/v1/rounds/{roundId}/ranking |
| GET | /api/v1/rounds/{roundId}/criteria |
| GET | /api/v1/events/{eventId}/tracks |
| GET | /api/v1/tracks |
| GET | /api/v1/roles |
| GET | /api/v1/enums |
