# Update Plan — Season Enum, Auto LeaderBoard, Year Field

## Chiến lược tổng thể

### 1. Entity & Migration
- **Entity**: Đổi kiểu `Season` từ `string?` → `SeasonEnum?` (EF Core lưu enum dưới dạng integer trong DB, cột `Season` từ `text` → `integer`)
- **Add migration mới**: Tạo migration riêng để cập nhật DB schema
- **Seed data**: Cập nhật seed cho khớp enum mới
- **Lưu ý**: Những migrations cũ (13 files) giữ nguyên — không sửa lại, chỉ thêm migration mới

### 2. Season Enum
- Tạo enum `SeasonEnum { Spring = 0, Summer = 1, Fall = 2, Winter = 3 }`
- Thay thế tất cả `string? Season` trong code bằng `SeasonEnum? Season`

### 3. Auto LeaderBoard
- Khi tạo event (`CreateEvent`), tự động tạo 1 `LeaderBoards` record
- `Year` lấy từ `StartTime.Year` (hoặc year hiện tại nếu StartTime null)

### 4. Response thêm field `Year`
- Tất cả Event response class thêm `int? Year` — lấy từ `StartTime.Value.Year`

---

## Chi tiết thay đổi

### Phần I: Khởi tạo — Enum + Entity

#### I.1 Tạo enum mới
**File:** `Hackathon.Repository/Enum/SeasonEnum.cs` (tạo mới)

```csharp
namespace Hackathon.Repository.Enum;

public enum SeasonEnum
{
    Spring,
    Summer,
    Fall,
    Winter
}
```

#### I.2 Sửa entity
**File:** `Hackathon.Repository/Entity/Events.cs`

| Trước | Sau |
|-------|-----|
| `public string? Season { get; set; }` | `public SeasonEnum? Season { get; set; }` |

#### I.3 Sửa seed data
**File:** `Hackathon.Repository/Seed/EventSeed.cs`

| Dòng | Trước | Sau |
|------|-------|-----|
| 25 | `Season = "2026"` | `Season = SeasonEnum.Spring` |
| 61 | `Season = startYear.ToString()` | `Season = (SeasonEnum)(startYear % 4)` (hoặc mapping cụ thể theo event) |

---

### Phần II: Response — thay đổi kiểu Season + thêm Year

#### II.1 `EventResponse`
**File:** `Hackathon.Service/Events/Response.cs` — class `EventResponse` (dòng 5)

| Field | Trước | Sau |
|-------|-------|-----|
| `Season` | `string?` | `SeasonEnum?` |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật:** `Events/Service.cs` → `ToResponse()` (dòng 78)
```csharp
// thêm:
Year = eventEntity.StartTime.HasValue ? eventEntity.StartTime.Value.Year : null,
```

**API:** `GET /api/v1/events/{id}` (`GetEvent`)
**Doc:** `Docs/ApiDocs/Events/GET/api-v1-events-id-get.md`

---

#### II.2 `StudentEventResponse`
**File:** `Hackathon.Service/Events/Response.cs` — class `StudentEventResponse` (dòng 49)

| Field | Trước | Sau |
|-------|-------|-----|
| `Season` | `string?` | `SeasonEnum?` |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật — 2 chỗ:**
- `Events/Service.cs` → `GetEvents()` — dòng 1063
- `Events/Service.cs` → `GetJoinedEvents()` — dòng 1189
```csharp
// thêm ở cả 2:
Year = x.StartTime.HasValue ? x.StartTime.Value.Year : null,
```

**API trực tiếp:**
- `GET /api/v1/events` (`GetEvents`)
- `GET /api/v1/events/joined` (`GetJoinedEvents`)

**Doc:**
- `Docs/ApiDocs/Events/GET/api-v1-events-get.md`
- `Docs/ApiDocs/Events/GET/api-v1-events-joined-get.md`

---

#### II.3 `AdminEventResponse`
**File:** `Hackathon.Service/Events/Response.cs` — class `AdminEventResponse` (dòng 60)

| Field | Trước | Sau |
|-------|-------|-----|
| `Season` | `string?` | `SeasonEnum?` |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật:** `Events/Service.cs` → `GetEventsForAdmin()` — dòng 1116
```csharp
// thêm:
Year = x.StartTime.HasValue ? x.StartTime.Value.Year : null,
```

**API:** `GET /api/v1/admin/events` (`GetEventsForAdmin`)
**Doc:** `Docs/ApiDocs/Events/GET/api-v1-admin-events-get.md`

---

#### II.4 `EventParticipantResponse` (kế thừa `EventResponse`)
**File:** `Hackathon.Service/Events/Response.cs` — class `EventParticipantResponse` (dòng 43)

Kế thừa `EventResponse` → tự động có `SeasonEnum?` + `Year` sau khi sửa class cha.

**Service cập nhật:** `Events/Service.cs` → `GetMostParticipants()` — dòng 1221

**API:** `GET /api/v1/events/most-participants`
**Doc:** `Docs/ApiDocs/Events/GET/api-v1-events-most-participants.md`

---

#### II.5 `LecturerEventResponse`
**File:** `Hackathon.Service/Lecturers/Response.cs` — class `LecturerEventResponse` (dòng 8)

| Field | Trước | Sau |
|-------|-------|-----|
| `Season` | `string?` | `SeasonEnum?` |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật — 3 chỗ:**
- `Lecturers/Service.cs` → `GetLecturerEvents()` — dòng 71
- `Lecturers/Service.cs` → `SearchLecturerEvents()` — dòng 130
- `Lecturers/Service.cs` → `GetCurrentLecturerEvents()` — dòng 165
```csharp
// thêm ở cả 3:
Year = x.Event.StartTime.HasValue ? x.Event.StartTime.Value.Year : null,
```

**API:**
- `GET /api/v1/lecturers/events`
- `GET /api/v1/lecturers/events/search`
- `GET /api/v1/lecturers/events/current`

**Doc:**
- `Docs/ApiDocs/Lecturers/GET/api-v1-lecturers-events-get.md`
- `Docs/ApiDocs/Lecturers/GET/api-v1-lecturers-events-search-get.md`
- `Docs/ApiDocs/Lecturers/GET/api-v1-lecturers-events-current-get.md`

---

#### II.6 `StaffEventResponse`
**File:** `Hackathon.Service/Staff/Response.cs` — class `StaffEventResponse` (dòng 7)

| Field | Trước | Sau |
|-------|-------|-----|
| `Season` | `string?` | `SeasonEnum?` |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật — 3 chỗ:**
- `Staff/Service.cs` → `GetStaffEvents()` — dòng 73
- `Staff/Service.cs` → `SearchStaffEvents()` — dòng 121
- `Staff/Service.cs` → `GetCurrentStaffEvents()` — dòng 242
```csharp
// thêm ở cả 3:
Year = x.Event.StartTime.HasValue ? x.Event.StartTime.Value.Year : null,
```

**API:**
- `GET /api/v1/staff/events`
- `GET /api/v1/staff/events/search`
- `GET /api/v1/staff/events/current`

**Doc:**
- `Docs/ApiDocs/Staff/GET/api-v1-staff-events-get.md`
- `Docs/ApiDocs/Staff/GET/api-v1-staff-events-search-get.md`
- `Docs/ApiDocs/Staff/GET/api-v1-staff-events-current-get.md`

---

#### II.7 `CreateEventResponse`
**File:** `Hackathon.Service/Events/Response.cs` — class `CreateEventResponse` (dòng 23)

| Field | Trước | Sau |
|-------|-------|-----|
| `Id` | `Guid` | `Guid` (giữ nguyên) |
| `LeaderBoardId` | *(không có)* | `Guid? LeaderBoardId` (mới) |
| `Year` | *(không có)* | `int? Year` (mới) |

**Service cập nhật:** `Events/Service.cs` → `CreateEvent()` — dòng 377
→ Sau khi tạo event + leaderboard, set:
```csharp
LeaderBoardId = leaderboard.Id,
Year = eventEntity.StartTime?.Year ?? now.Year,
```

**API:** `POST /api/v1/admin/events`
**Doc:** `Docs/ApiDocs/Events/POST/api-v1-admin-events-post.md`

---

### Phần III: Request — đổi kiểu Season (giữ nguyên Year)

| # | File | Class | Dòng | Trước | Sau |
|---|------|-------|------|-------|-----|
| 1 | `Events/Request.cs` | `CreateEventRequest` | 42 | `string? Season` | `SeasonEnum? Season` |
| 2 | `Events/Request.cs` | `UpdateEventRequest` | 57 | `string? Season` | `SeasonEnum? Season` |

**Year filter trong Request — đã có sẵn, không cần thêm:**

| Request class | Field `int? Year` | Dòng |
|---------------|-------------------|------|
| `GetEventsRequest` | ✅ có sẵn | 12 |
| `GetEventsForAdminRequest` | ✅ có sẵn | 19 |
| `GetJoinedEventsRequest` | ✅ có sẵn | 27 |
| `SearchLecturerEventsRequest` | ✅ có sẵn | 11 |
| `SearchStaffEventsRequest` | ✅ có sẵn | 13 |

---

### Phần IV: Service — UpdateEvent

**File:** `Hackathon.Service/Events/Service.cs` — dòng 751

| Trước | Sau |
|-------|-----|
| `if (request.Season != null)` | `if (request.Season.HasValue)` |
| `eventEntity.Season = request.Season;` | `eventEntity.Season = request.Season.Value;` |

---

### Phần V: Service — Keyword search có Season

Cả 3 method trong `Events/Service.cs` có filter keyword trên `Season` đều cần sửa.

**Cách xử lý:** Thử parse `normalizedKeyword` sang `SeasonEnum`, nếu parse được thì filter `x.Season == parsedSeason`, nếu không thì bỏ qua (Season không còn search text được nữa).

| Method | Dòng |
|--------|------|
| `GetEvents()` | 1032 |
| `GetEventsForAdmin()` | 1085 |
| `GetJoinedEvents()` | 1157 |

**Code thay thế (dùng cho cả 3):**
```csharp
// Thử search theo Season enum
if (Enum.TryParse<SeasonEnum>(normalizedKeyword, ignoreCase: true, out var parsedSeason))
{
    query = query.Where(x => x.Season == parsedSeason);
}
```
→ Xoá dòng `|| (x.Season != null && x.Season.ToLower().Contains(normalizedKeyword))` cũ.

---

### Phần VI: Auto LeaderBoard trong CreateEvent

**File:** `Hackathon.Service/Events/Service.cs` — dòng 337 `CreateEvent()`

**Vị trí thêm:** Sau `_dbContext.Events.AddAsync(eventEntity);` (dòng 371), trước `_roundEndScheduler.ScheduleEvent(...)` (dòng 374)

**Code thêm:**
```csharp
var leaderboard = new Repository.Entity.LeaderBoards
{
    Id = Guid.NewGuid(),
    EventId = eventEntity.Id,
    Year = eventEntity.StartTime?.Year ?? now.Year,
    IsLocked = false,
    IsPublished = false,
    IsDisable = false,
    CreatedAt = now,
    UpdatedAt = now,
};
await _dbContext.LeaderBoards.AddAsync(leaderboard);
```

---

### Phần VII: Tổng kết số lượng thay đổi

| Nhóm | Số files | Chi tiết |
|------|---------|----------|
| **Tạo mới** | 1 | `SeasonEnum.cs` |
| **Sửa entity** | 1 | `Events.cs` |
| **Sửa request** | 1 | `Request.cs` (2 field) |
| **Sửa response** | 3 | `Events/Response.cs` (5 class), `Lecturers/Response.cs`, `Staff/Response.cs` |
| **Sửa service** | 3 | `Events/Service.cs` (14 chỗ), `Lecturers/Service.cs` (3 chỗ), `Staff/Service.cs` (3 chỗ) |
| **Sửa seed** | 1 | `EventSeed.cs` (2 chỗ) |
| **Add migration** | 1 | Migration mới (EF Core auto generate) |
| **Sửa docs** | 16 | Liệt kê ở mục IX |
| **Tổng cộng** | **~27** | |

---

### Phần VIII: Thứ tự thực hiện

```
Step 1: Tạo SeasonEnum.cs
Step 2: Sửa entity Events.cs (Season → SeasonEnum?)
Step 3: Sửa request/response DTOs
Step 4: Sửa services (mapping + keyword search + UpdateEvent + auto LeaderBoard)
Step 5: Sửa seed data
Step 6: Add migration
Step 7: Build + fix lỗi
Step 8: Sửa docs
```

---

### Phần IX: Danh sách Doc cần update

| # | Doc path | Nội dung sửa |
|---|---------|--------------|
| 1 | `Events/GET/api-v1-events-get.md` | `season` → enum (integer), thêm field `year`, update enum table, thêm filter `year` |
| 2 | `Events/GET/api-v1-events-id-get.md` | `season` → enum, thêm `year` |
| 3 | `Events/GET/api-v1-events-joined-get.md` | `season` → enum, thêm `year`, thêm filter `year` |
| 4 | `Events/GET/api-v1-events-most-participants.md` | `season` → enum, thêm `year` |
| 5 | `Events/GET/api-v1-admin-events-get.md` | `season` → enum, thêm `year`, thêm filter `year` |
| 6 | `Events/POST/api-v1-admin-events-post.md` | `season` → enum, thêm `leaderBoardId`, `year` response |
| 7 | `Events/PATCH/api-v1-admin-events-id-patch.md` | `season` → enum |
| 8 | `Lecturers/GET/api-v1-lecturers-events-get.md` | `season` → enum, thêm `year` |
| 9 | `Lecturers/GET/api-v1-lecturers-events-search-get.md` | `season` → enum, thêm `year` |
| 10 | `Lecturers/GET/api-v1-lecturers-events-current-get.md` | `season` → enum, thêm `year` |
| 11 | `Staff/GET/api-v1-staff-events-get.md` | `season` → enum, thêm `year` |
| 12 | `Staff/GET/api-v1-staff-events-search-get.md` | `season` → enum, thêm `year` |
| 13 | `Staff/GET/api-v1-staff-events-current-get.md` | `season` → enum, thêm `year` |
| 14 | `doc.md` | Update Season thành enum |
| 15 | `doc01.md` | Update Season thành enum |
| 16 | `Superpowers/Specs/2026-06-25-lecturers-module-design.md` | Update Season thành enum |

---

### Phần X: Lưu ý

- `EventParticipantResponse` kế thừa `EventResponse` → tự động có `SeasonEnum?` + `Year` sau khi sửa class cha
- Các migration cũ (13 files .Designer.cs) **không cần sửa** — EF Core tự handle qua migration mới
- Các chỗ `Season = x.Season`, `Season = eventEntity.Season`, `Season = request.Season` trong `.Select()` / assignment **tự động map enum → enum**, không cần động tay (trừ `CreateEvent` và `ToResponse` đã liệt kê)
- Khi serialization, enum ra integer (0, 1, 2, 3) — giống pattern `EventRoleEnum` và `EventStatusEnum` đang dùng
