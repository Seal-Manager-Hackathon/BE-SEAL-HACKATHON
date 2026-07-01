# Update 2 — 2026-07-02

## 1. Xoá EventStatusEnum.Cancelled

**Entity:** `EventStatusEnum` — xoá value `Cancelled` (giá trị enum `3`).

**API bị ảnh hưởng:**

| API | Thay đổi |
|-----|----------|
| `PATCH /api/v1/admin/events/{eventId}/cancel` | **❗ XOÁ** endpoint này — không còn cancel event |
| `GET /api/v1/events` | Response: `status` chỉ còn 0=Draft, 1=Published, 2=Closed |
| `GET /api/v1/events/{id}` | Response: `status` chỉ còn 0, 1, 2 |
| `GET /api/v1/events/joined` | Response: `status` chỉ còn 0, 1, 2 |
| `GET /api/v1/events/most-participants` | Response: `status` chỉ còn 0, 1, 2 |
| `GET /api/v1/admin/events` | Response: `status` chỉ còn 0, 1, 2 |
| `PATCH /api/v1/admin/events/{id}` | Request field `status` chỉ nhận 0, 1, 2; không còn 3 |
| `PATCH /api/v1/admin/events/{id}/close` | Response: `eventStatus` chỉ còn 0, 1, 2 |
| `PATCH /api/v1/admin/events/{id}/unpublish` | Response: `eventStatus` chỉ còn 0, 1, 2 |
| `GET /api/v1/staff/events/current` | Response: `eventStatus` chỉ còn 0, 1, 2 |
| `GET /api/v1/staff/events/search` | Response: `eventStatus` chỉ còn 0, 1, 2 |
| Tất cả Lecturer events APIs | Response: `eventStatus` chỉ còn 0, 1, 2 |
| `GET /api/v1/enums` | Response: EventStatusEnum chỉ còn 0, 1, 2 |
| `enum-values.md` | Xoá Cancelled khỏi danh sách |

**Docs bị xoá:**
- `Events/PATCH/api-v1-admin-events-id-cancel-patch.md` ❌

---

## 2. Round CRUD thay đổi lớn

### 2.1 POST `/api/v1/admin/events/{eventId}/rounds` — Create Round

**Request thay đổi:**

**Trước:**
```json
{
  "name": "Vòng 1",
  "roundNo": 1,       // 👈 người dùng tự nhập
  "startTime": "...",
  "endTime": "...",
  "startSubmission": "...",
  "endSubmission": "...",
  "limitTeam": 20
}
```

**Sau:**
```json
{
  "name": "Vòng 1",
  // ❌ KHÔNG còn field roundNo — tự động
  "startTime": "...",
  "endTime": "...",
  "startSubmission": "...",
  "endSubmission": "...",
  "limitTeam": 20
}
```

**Thay đổi:**
- ❌ **Xoá field `roundNo`** khỏi request — RoundNo tự động = max current + 1 (bắt đầu từ 1)
- ❌ **Chặn nếu event đã bắt đầu** (`StartTime <= now`) → 400 `EVENT_ALREADY_STARTED`
- ✅ **Tự động +1** `NumberRound` của event

**Doc:**
- `Admin/POST/api-v1-admin-events-id-rounds-post.md` ✅ **Viết lại**

### 2.2 PATCH `/api/v1/admin/rounds/{roundId}` — Update Round

**Request tách riêng `UpdateRoundRequest` (không còn dùng chung với CreateRoundRequest):**

**Trước:**
```json
{
  "name": "...",
  "roundNo": 1        // ghi đè roundNo
}
```

**Sau (logic roundNo thay đổi):**
```json
{
  "name": "...",
  "roundNo": 2        // HOÁN ĐỔI với round đang giữ số 2
}
```

**Thay đổi:**
- 🔄 **`roundNo` bây giờ là SWAP** (hoán đổi): gửi `roundNo` mới → tìm round đang giữ số đó → đổi chỗ RoundNo cho nhau. Không phải ghi đè.
- ❌ **Chặn critical fields nếu event đã bắt đầu:** khi `StartTime <= now`, chỉ cho sửa `name`/`description`. Các field `startTime`, `endTime`, `startSubmission`, `endSubmission`, `roundNo`, `limitTeam` bị từ chối → 400 `EVENT_ALREADY_STARTED`.

**Doc:**
- `Admin/PATCH/api-v1-admin-rounds-id-patch.md` ✅ **Viết lại**

### 2.3 DELETE `/api/v1/admin/rounds/{roundId}` — Delete Round

**Thay đổi logic:**
- ❌ **Chặn nếu event đã bắt đầu** → 400 `EVENT_ALREADY_STARTED`
- 🔄 **Chuẩn hoá RoundNo:** các round có `RoundNo > round bị xoá` giảm 1 (để RoundNo luôn liên tục)
- ✅ **Tự động -1** `NumberRound` của event

**Doc:**
- `Admin/DELETE/api-v1-admin-rounds-id-delete.md` ✅ **Viết lại**

### 2.4 POST `/api/v1/admin/events` — Create Event

**Thay đổi:**
- ✅ Request vẫn có `numberRound` nhưng **bị IGNORE** — luôn set `NumberRound = 0` khi tạo. NumberRound tự động quản lý qua round CRUD.

### 2.5 PATCH `/api/v1/admin/events/{id}` — Update Event

**Thay đổi:**
- ❌ Request field `numberRound` **bị xoá** — không còn nhập tay qua update event.

---

## 3. Team Detail mở quyền

### GET `/api/v1/teams/{teamId}`

**Thay đổi:**
- Trước: chỉ member team hoặc Staff/Admin xem được
- Sau: **tất cả role đã login** đều xem được team detail
- Vẫn chặn nếu team bị disable (404)

---

## 4. Register Event check Published

### POST `/api/v1/register-teams` (RegisterEvent)

**Thay đổi:**
- ✅ **Thêm check:** event phải có `Status == Published` mới được đăng ký. Draft/Closed → 400 `EVENT_NOT_OPEN_FOR_REGISTRATION`.

---

## 5. DeleteRound: soft-delete criteria templates theo

### DELETE `/api/v1/admin/rounds/{roundId}`

**Thay đổi:**
- **Trước:** chỉ soft-delete round, không động tới criteria
- **Sau:** ✅ **Soft-delete tất cả CriteriaTemplates + CriteriaItems** của round đó (`IsDisable = true`)

---

## 7. Submission: CreateSubmission check Leader

### POST `/api/v1/rounds/{roundId}/submit-assignment`

**Thay đổi:**
- **Trước:** bất kỳ thành viên nào trong team cũng nộp được
- **Sau:** ✅ **Chỉ Leader mới nộp được** (thêm `&& td.IsLeader`)
- API `POST /api/v1/submissions/rounds/{id}/register-teams/{id}` (SubmitRoundProject) đã có check leader từ trước → giữ nguyên.

---

## 8. Judge Event Submissions: chỉ lấy latest

### GET `/api/v1/judge/events/{eventId}/submissions`

**Thay đổi:**
- **Trước:** trả tất cả submissions
- **Sau:** ✅ GroupBy team+round → chỉ lấy **latest submission** (giống các API Judge khác)

---

## Tổng kết ảnh hưởng FE

### Request thay đổi:
| API | Field cũ còn không? | Ghi chú |
|-----|---------------------|---------|
| `POST events/{id}/rounds` | ❌ `roundNo` bị xoá | Auto-generated |
| `PATCH events/{id}` | ❌ `numberRound` bị xoá | Qua round CRUD |
| `PATCH rounds/{id}` | 🔄 `roundNo` đổi ý nghĩa | Swap, ko ghi đè |

### Response thay đổi:
| API | Thay đổi |
|-----|----------|
| Tất cả event APIs | `status` không còn value `3` (Cancelled) |
| `GET /api/v1/enums` | EventStatusEnum chỉ còn 3 values |

### Endpoint bị xoá:
| Endpoint | Thay thế bởi |
|----------|-------------|
| `PATCH /api/v1/admin/events/{id}/cancel` | ❌ Không có — dùng `Close` hoặc `Disable` event |
