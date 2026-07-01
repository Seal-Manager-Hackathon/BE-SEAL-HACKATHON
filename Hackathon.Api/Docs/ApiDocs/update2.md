# Update 2 — 2026-07-02

## 1. Xoá EventStatusEnum.Cancelled

Xoá value `Cancelled` (giá trị enum `3`) khỏi `EventStatusEnum`.

**Entity sửa:** `Hackathon.Repository/Enum/EventStatusEnum.cs` — xoá dòng `Cancelled`.

**Code sửa:**
- `Events/Service.cs`: xoá method `CancelEvent()`, xoá check `Cancelled` trong `GetEvent()`
- `Events/IService.cs`: xoá `Task<string> CancelEvent(Guid eventId)`
- `LeaderBoards/Service.cs`: xoá check `Cancelled`
- `EventsController.cs`: xoá endpoint `PATCH /api/v1/admin/events/{eventId}/cancel`

**Docs bị xoá:**
- `Events/PATCH/api-v1-admin-events-id-cancel-patch.md` ❌

---

### 1.1 Tất cả API trả về `status` (EventStatusEnum trong response)

**Response cũ:**
```json
"status": 0  /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
```

**Response mới:**
```json
"status": 0  /* 0: Draft, 1: Published, 2: Closed */
```

**Các API bị ảnh hưởng:**
| API | File doc |
|-----|----------|
| `GET /api/v1/events` | `Events/GET/api-v1-events-get.md` |
| `GET /api/v1/events/{id}` | `Events/GET/api-v1-events-id-get.md` |
| `GET /api/v1/events/joined` | `Events/GET/api-v1-events-joined-get.md` |
| `GET /api/v1/events/most-participants` | `Events/GET/api-v1-events-most-participants.md` |
| `GET /api/v1/admin/events` | `Events/GET/api-v1-admin-events-get.md` |
| `PATCH /api/v1/admin/events/{id}/close` | `Events/PATCH/api-v1-admin-events-id-close-patch.md` |
| `PATCH /api/v1/admin/events/{id}/unpublish` | `Events/PATCH/api-v1-admin-events-id-unpublish-patch.md` |
| `GET /api/v1/staff/events/current` | `Staff/GET/api-v1-staff-events-current-get.md` |
| `GET /api/v1/staff/events/search` | `Staff/GET/api-v1-staff-events-search-get.md` |
| `GET /api/v1/lecturers/events` | `Lecturers/GET/api-v1-lecturers-events-get.md` |
| `GET /api/v1/lecturers/events/search` | `Lecturers/GET/api-v1-lecturers-events-search-get.md` |
| `GET /api/v1/lecturers/events/current` | `Lecturers/GET/api-v1-lecturers-events-current-get.md` |

### 1.2 API `PATCH /api/v1/admin/events/{id}` — Update Event (field status)

**Request cũ:**
```json
{ "status": 3  /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */ }
```

**Request mới:**
```json
{ "status": 2  /* 0: Draft, 1: Published, 2: Closed */ }
```

### 1.3 API `GET /api/v1/enums`

**Response cũ:**
```json
"EventStatusEnum": { "0": "Draft", "1": "Published", "2": "Closed", "3": "Cancelled" }
```

**Response mới:**
```json
"EventStatusEnum": { "0": "Draft", "1": "Published", "2": "Closed" }
```

### 1.4 File `enum-values.md`

Xoá dòng `- \`3\`: Cancelled (Bị hủy)`

---

## 2. Round CRUD

### 2.1 POST `/api/v1/admin/events/{eventId}/rounds` — Create Round

**Request cũ:**
```json
{
  "name": "Vòng 1",
  "roundNo": 1,
  "startTime": "2026-07-01T09:00:00+00:00",
  "endTime": "2026-07-03T18:00:00+00:00",
  "startSubmission": "2026-07-01T09:00:00+00:00",
  "endSubmission": "2026-07-03T12:00:00+00:00",
  "limitTeam": 20
}
```

**Request mới:**
```json
{
  "name": "Vòng 1",
  "startTime": "2026-07-01T09:00:00+00:00",
  "endTime": "2026-07-03T18:00:00+00:00",
  "startSubmission": "2026-07-01T09:00:00+00:00",
  "endSubmission": "2026-07-03T12:00:00+00:00",
  "limitTeam": 20
}
```

**Khác biệt:**
- ❌ Xoá field `roundNo` — tự động gán = max RoundNo hiện tại + 1

**Code sửa:**
- `Admin/Request.cs`: xoá `RoundNo` khỏi `CreateRoundRequest`
- `Admin/Service.cs` `CreateRound()`: xoá `ValidateRoundNo()`, thêm logic `var maxRoundNo = ...MaxAsync()`, set `RoundNo = maxRoundNo + 1`

**Logic mới:**
- RoundNo tự động bắt đầu từ 1, mỗi round mới = max + 1
- Tự động +1 `NumberRound` của event
- Chặn nếu event đã bắt đầu (`StartTime <= now`) → 400 `EVENT_ALREADY_STARTED`

**File doc:** `Admin/POST/api-v1-admin-events-id-rounds-post.md` ✅ Viết lại

---

### 2.2 PATCH `/api/v1/admin/rounds/{roundId}` — Update Round

**Chú ý:** Request class tách riêng — không còn dùng chung `CreateRoundRequest`. Dùng `UpdateRoundRequest` (có field `RoundNo`).

**Request cũ** (dùng `CreateRoundRequest`):
```json
{
  "name": "...",
  "roundNo": 1,
  "startTime": "..."
}
```

**Request mới** (dùng `UpdateRoundRequest`):
```json
{
  "name": "...",
  "roundNo": 2,
  "startTime": "..."
}
```
Giống nhau về field — khác ở logic xử lý `roundNo`.

**Code sửa:**
- `Admin/Request.cs`: thêm class `UpdateRoundRequest` (giống `CreateRoundRequest` nhưng có `RoundNo`)
- `Admin/IService.cs`: đổi `Task UpdateRound(Guid roundId, UpdateRoundRequest request)`
- `AdminController.cs`: đổi parameter từ `CreateRoundRequest` → `UpdateRoundRequest`
- `Admin/Service.cs` `UpdateRound()`: bỏ `ValidateRoundNo()`, thêm swap logic

**Logic roundNo thay đổi:**
```
Trước: ghi đè — round này được gán roundNo mới, không quan tâm round khác
Sau: SWAP — tìm round đang giữ số target, hoán đổi RoundNo cho nhau
```

**Logic khác:**
- Chặn critical fields (`startTime`, `endTime`, `startSubmission`, `endSubmission`, `roundNo`, `limitTeam`) nếu event đã bắt đầu — chỉ cho sửa `name`/`description`
- Nếu event chưa bắt đầu → cho sửa tất cả

**File doc:** `Admin/PATCH/api-v1-admin-rounds-id-patch.md` ✅ Viết lại

---

### 2.3 DELETE `/api/v1/admin/rounds/{roundId}` — Delete Round

**Request/Response:** Không đổi.

**Code sửa:** `Admin/Service.cs` `DeleteRound()` — thêm:
1. Chặn nếu event đã bắt đầu → 400 `EVENT_ALREADY_STARTED`
2. Soft-delete CriteriaTemplates + CriteriaItems của round
3. Chuẩn hoá RoundNo: các round > deleted giảm 1
4. -1 `NumberRound` của event

**File doc:** `Admin/DELETE/api-v1-admin-rounds-id-delete.md` ✅ Viết lại

---

### 2.4 POST `/api/v1/admin/events` — Create Event

**Request cũ:**
```json
{
  "name": "Hackathon 2026",
  "numberRound": 3,
  "startTime": "...",
  "endTime": "..."
}
```

**Request mới:**
```json
{
  "name": "Hackathon 2026",
  "numberRound": 3,
  "startTime": "...",
  "endTime": "..."
}
```
Field `numberRound` vẫn còn trong `CreateEventRequest` nhưng **bị BE IGNORE**.

**Code sửa:** `Events/Service.cs` `CreateEvent()`: `NumberRound = 0` (hardcode thay vì `request.NumberRound`).

**Lưu ý FE:** Field `numberRound` vẫn gửi được nhưng không có tác dụng. Có thể bỏ hoặc giữ.

---

### 2.5 PATCH `/api/v1/admin/events/{id}` — Update Event

**Request cũ:**
```json
{
  "name": "Hackathon 2026",
  "numberRound": 5
}
```

**Request mới:**
```json
{
  "name": "Hackathon 2026"
}
```

**Khác biệt:** ❌ Xoá field `numberRound` — không còn cho phép sửa NumberRound qua update event.

**Code sửa:** `Events/Service.cs` `UpdateEvent()` — xoá block:
```csharp
if (request.NumberRound.HasValue)
{
    eventEntity.NumberRound = request.NumberRound;
}
```

**Lưu ý FE:** Bỏ field `numberRound` khỏi form update event. NumberRound tự động quản lý qua round CRUD.

---

### 2.6 API mới: PATCH `/api/v1/admin/rounds/{roundId}/restore` — Restore Round

**Request:** Không có body.

**Response mới:**
```json
{
  "message": "ROUND_RESTORED_SUCCESSFULLY"
}
```

**Code:** `Admin/Service.cs` `RestoreRound()` + `Admin/IService.cs` + `AdminController.cs`.

**Logic:**
- Round đang disable → set `IsDisable = false`
- Gán lại `RoundNo = max RoundNo hiện tại + 1` (đặt cuối danh sách)
- +1 `NumberRound` của event
- **Criteria templates/items vẫn disable** — admin tự active lại

**File doc mới:** `Admin/PATCH/api-v1-admin-rounds-id-restore-patch.md` ✅

---

## 3. GET `/api/v1/teams/{teamId}` — Team Detail

**Response cũ và mới:** Không đổi.

**Code sửa:** `Teams/Service.cs` `GetTeamDetail()` — bỏ block check quyền:

```csharp
// Trước:
var isMember = team.TeamDetails.Any(x => x.UserId == userId && !x.IsDisable);
var isStaffOrAdmin = userRole == RoleEnum.Staff || userRole == RoleEnum.Admin;
if (!isMember && !isStaffOrAdmin)
    throw new ForbiddenException("TEAM_NOT_VISIBLE_TO_USER");

// Sau: (đã xoá — ai có token cũng xem được)
```

**Lưu ý FE:** Không thay đổi gì — chỉ BE mở quyền.

---

## 4. POST `/api/v1/register-teams` — Register Event

**Request/Response:** Không đổi.

**Code sửa:** `RegisterTeams/Service.cs` `RegisterEvent()` — thêm check:

```csharp
// Thêm sau block validate event
if (eventEntity.Status != EventStatusEnum.Published)
    throw new BadRequestException("EVENT_NOT_OPEN_FOR_REGISTRATION");
```

**Trước:** Cho đăng ký vào event Draft/Closed.
**Sau:** Chỉ cho đăng ký khi event `Published`.

---

## 5. POST `/api/v1/rounds/{roundId}/submit-assignment` — Create Submission

**Request/Response:** Không đổi.

**Code sửa:** `Rounds/Service.cs` `CreateSubmission()`:

```csharp
// Trước: bất kỳ thành viên active
td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active

// Sau: chỉ leader
td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active && td.IsLeader
```

**Lưu ý FE:** Chỉ leader mới thấy nút nộp bài. Các thành viên khác bị 403.

---

## 6. GET `/api/v1/judge/events/{eventId}/submissions`

**Response:** Cấu trúc không đổi. Số lượng items giảm vì chỉ trả latest per team per round.

**Code sửa:** `Judges/Service.cs` `GetEventSubmissions()`:

```csharp
// Trước:
var submissions = await submissionsQuery
    .OrderByDescending(x => x.SubmittedAt)
    .Select(...)
    .ToListAsync();

// Sau:
var submissions = await submissionsQuery
    .OrderByDescending(x => x.SubmittedAt)
    .ToListAsync();
var latestPerTeamPerRound = submissions
    .GroupBy(x => new { x.RoundDetail.RegisterTeamId, x.RoundDetail.RoundId })
    .Select(g => g.First())
    .ToList();
```

---

## 7. Background Job mới: AutoRejectPendingRegistrationsJob

**Chạy mỗi 12 tiếng — không ảnh hưởng request/response.**

**File:** `BackgroundJobService/AutoRejectPendingRegistrationsJob.cs`

**Việc 1:** Event có `EndTime` đã qua → tự động set `Status = Closed`
**Việc 2:** Event có `RegisterLimitTime` đã qua → tự động reject `RegisterTeams` đang `Pending` với lý do `"registration deadline has passed"`

---

## Tổng kết ảnh hưởng FE

### Request thay đổi:
| API | Field cũ còn không? | Ghi chú |
|-----|---------------------|---------|
| `POST events/{id}/rounds` | ❌ `roundNo` bị xoá | Auto-generated |
| `PATCH events/{id}` | ❌ `numberRound` bị xoá | Qua round CRUD |
| `PATCH rounds/{id}` | 🔄 `roundNo` đổi ý nghĩa | Swap, ko ghi đè |
| `POST events` | 🔄 `numberRound` bị IGNORE | Có thể bỏ field |

### Response thay đổi:
| API | Thay đổi |
|-----|----------|
| Tất cả event APIs | `status` không còn value `3` (Cancelled) |
| `GET /api/v1/enums` | EventStatusEnum chỉ còn 3 values |

### Endpoint bị xoá:
| Endpoint | Thay thế bởi |
|----------|-------------|
| `PATCH /api/v1/admin/events/{id}/cancel` | ❌ Không — dùng `Close` hoặc `Disable` |

### Endpoint mới:
| Endpoint | Mô tả |
|----------|-------|
| `PATCH /api/v1/admin/rounds/{id}/restore` | Khôi phục round đã disable |
| `GET /api/v1/notifications/me/unread-count` | Đếm thông báo chưa đọc |
| `PATCH /api/v1/notifications/all/disable` | Disable hết thông báo |
