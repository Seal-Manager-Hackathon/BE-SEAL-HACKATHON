# Update 2 — Thay đổi Season & Year (04/07/2026)

---

## 1. Season — Entity: string → SeasonEnum

**File:** `Hackathon.Repository/Entity/Events.cs`

**Thay đổi:** Cột `Season` từ `string?` → `SeasonEnum?` (enum).

| Kiểu | Trước | Sau |
|---|---|---|
| Entity | `public string? Season { get; set; }` | `public SeasonEnum? Season { get; set; }` |

### SeasonEnum values:
| Giá trị | String |
|---|---|
| `0` | `Spring` |
| `1` | `Summer` |
| `2` | `Autumn` |
| `3` | `Winter` |

**Logic:** FE gửi string `"Summer"` — model binding tự parse vào enum.

---

## 2. Season — Request: string → SeasonEnum

**File:** `Hackathon.Service/Events/Request.cs`

### CreateEventRequest
| Field | Trước | Sau |
|---|---|---|
| `season` | `string?` | `SeasonEnum?` |

### UpdateEventRequest
| Field | Trước | Sau |
|---|---|---|
| `season` | `string?` | `SeasonEnum?` |

**Logic:** Gửi JSON `{ "season": "Summer" }` (string), ko gửi số.

---

## 3. Season & Status — Response: string → SeasonEnum / EventStatusEnum

**File:** `Hackathon.Service/Events/Response.cs`

### EventResponse
| Field | Trước | Sau |
|---|---|---|
| `status` | `string?` | `EventStatusEnum?` |
| `season` | `string?` | `SeasonEnum?` |

### StudentEventResponse
| Field | Trước | Sau |
|---|---|---|
| `status` | `string?` | `EventStatusEnum?` |
| `season` | `string?` | `SeasonEnum?` |

### AdminEventResponse
| Field | Trước | Sau |
|---|---|---|
| `status` | `string?` | `EventStatusEnum?` |
| `season` | `string?` | `SeasonEnum?` |

**Logic:** Response trả về string enum (VD: `"Summer"`, `"Published"`), không phải số.

---

## 4. Season — Staff/Lecturer Response: string → SeasonEnum

**File:** `Hackathon.Service/Staff/Response.cs`

### StaffEventResponse
| Field | Trước | Sau |
|---|---|---|
| `season` | `string?` | `SeasonEnum?` |

**File:** `Hackathon.Service/Lecturers/Response.cs`

### LecturerEventResponse
| Field | Trước | Sau |
|---|---|---|
| `season` | `string?` | `SeasonEnum?` |

---

## 5. Season — Seed data: string → SeasonEnum

**File:** `Hackathon.Repository/Seed/EventSeed.cs`

| Dòng | Trước | Sau |
|---|---|---|
| SEAL Hackathon 2026 | `Season = "2026"` | `Season = SeasonEnum.Winter` |
| CreateEvent() | `Season = startYear.ToString()` | `Season = SeasonEnum.Winter` |

**File:** `Hackathon.Repository/Seed/FPTSeed.cs`

| Dòng | Trước | Sau |
|---|---|---|
| Event 1 (Spring) | `Season = "Spring 2026"` | `Season = SeasonEnum.Spring` |
| Event 2 (Summer) | `Season = "Summer 2026"` | `Season = SeasonEnum.Summer` |

---

## 6. Season — Service: bỏ filter ToLower()

**File:** `Hackathon.Service/Events/Service.cs`

**Thay đổi:** Xoá 3 chỗ filter `x.Season.ToLower().Contains(normalizedKeyword)` vì enum không gọi được `.ToLower()`.

| API | Thay đổi |
|---|---|
| `GetEvents()` | Bỏ filter Season trong keyword search |
| `GetEventsForAdmin()` | Bỏ filter Season trong keyword search |
| `GetJoinedEvents()` | Bỏ filter Season trong keyword search |

**Logic:** Search keyword chỉ áp dụng cho `name` và `description`, không search season nữa.

---

## 7. Year — Thêm field mới vào Response

**Thay đổi:** Thêm `Year` (`int?`) vào 5 response classes.

**Cách tính:** `startTime?.Year ?? createdAt.Year`

**File:** `Hackathon.Service/Events/Response.cs`

### EventResponse
```
Trước: { ..., "isDisable": false, "createdAt": "..." }
Sau:  { ..., "isDisable": false, "year": 2026, "createdAt": "..." }
```

### StudentEventResponse
```
Trước: { ..., "season": "Summer", "createdAt": "..." }
Sau:  { ..., "season": "Summer", "year": 2026, "createdAt": "..." }
```

### AdminEventResponse
```
Trước: { ..., "season": "Summer", "isDisable": false, "createdAt": "..." }
Sau:  { ..., "season": "Summer", "isDisable": false, "year": 2026, "createdAt": "..." }
```

**File:** `Hackathon.Service/Staff/Response.cs`

### StaffEventResponse
```
Trước: { ..., "eventStatus": 1 }
Sau:  { ..., "eventStatus": 1, "year": 2026 }
```

**File:** `Hackathon.Service/Lecturers/Response.cs`

### LecturerEventResponse
```
Trước: { ..., "eventStatus": 1 }
Sau:  { ..., "eventStatus": 1, "year": 2026 }
```

---

## 8. Year — Gán trong Service

**File:** `Hackathon.Service/Events/Service.cs`

**ToResponse():** thêm `Year = eventEntity.StartTime?.Year ?? eventEntity.CreatedAt.Year`

**GetEvents() / GetEventsForAdmin() / GetJoinedEvents() / GetMostParticipants() / GetAdminEvent():**
Thêm `Year = x.StartTime != null ? x.StartTime.Value.Year : x.CreatedAt.Year` vào Select.

**File:** `Hackathon.Service/Staff/Service.cs`

**GetCurrentStaffEvents() / GetStaffEvents() / SearchStaffEvents():**
Thêm `Year = x.Event.StartTime != null ? x.Event.StartTime.Value.Year : x.Event.CreatedAt.Year` vào Select.

**File:** `Hackathon.Service/Lecturers/Service.cs`

**GetLecturerEvents() / SearchLecturerEvents() / GetCurrentLecturerEvents():**
Thêm `Year = x.Event.StartTime != null ? x.Event.StartTime.Value.Year : x.Event.CreatedAt.Year` vào Select.

---

## 9. Các API có response bị ảnh hưởng

### Season thay đổi (string → SeasonEnum):
| API | Doc |
|---|---|
| `GET /api/v1/events` | `Events/GET/api-v1-events-get.md` |
| `GET /api/v1/events/{eventId}` | `Events/GET/api-v1-events-id-get.md` |
| `GET /api/v1/events/joined` | `Events/GET/api-v1-events-joined-get.md` |
| `GET /api/v1/events/most-participants` | `Events/GET/api-v1-events-most-participants.md` |
| `GET /api/v1/admin/events` | `Events/GET/api-v1-admin-events-get.md` |
| `GET /api/v1/admin/events/{eventId}` | `Admin/GET/api-v1-admin-events-id-get.md` |
| `POST /api/v1/admin/events` | `Events/POST/api-v1-admin-events-post.md` |
| `PATCH /api/v1/admin/events/{eventId}` | `Events/PATCH/api-v1-admin-events-id-patch.md` |
| `GET /api/v1/staff/events` | `Staff/GET/api-v1-staff-events-get.md` |
| `GET /api/v1/staff/events/current` | `Staff/GET/api-v1-staff-events-current-get.md` |
| `GET /api/v1/staff/events/search` | `Staff/GET/api-v1-staff-events-search-get.md` |
| `GET /api/v1/lecturers/events` | `Lecturers/GET/api-v1-lecturers-events-get.md` |
| `GET /api/v1/lecturers/events/current` | `Lecturers/GET/api-v1-lecturers-events-current-get.md` |
| `GET /api/v1/lecturers/events/search` | `Lecturers/GET/api-v1-lecturers-events-search-get.md` |

### Year thêm mới:
Cùng danh sách API trên (response thêm field `year`).

### Lưu ý FE:
- **Season:** gửi lên `"Summer"` (string), nhận về `"Summer"` (string). Không gửi số.
- **Year:** nhận về số nguyên `2026`. FE không gửi year lên (chỉ nhận). Year lấy từ `StartTime?.Year` của event (hoặc `CreatedAt.Year` nếu StartTime null).
