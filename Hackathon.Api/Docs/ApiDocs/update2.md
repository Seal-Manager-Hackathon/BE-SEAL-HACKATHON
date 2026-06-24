# Bảng Tổng Hợp Thay Đổi API Dành Cho Frontend AI (update2.md)

Tài liệu này tổng hợp toàn bộ 26 thay đổi/thêm mới API trong hệ thống để Frontend AI dễ dàng cập nhật code.

---

## I. BẢNG TRUY CỨU NHANH ENUM SỐ NGUYÊN (INTEGER)

| Tên Enum | Giá trị Số (Integer) & Tên Vai Trò/Trạng Thái |
| :--- | :--- |
| **RoleEnum** | `0`: Admin, `1`: Staff, `2`: Student, `3`: Lecturer |
| **EventStatusEnum** | `0`: Draft, `1`: Published, `2`: Closed, `3`: Cancelled |
| **RegisterTeamStatusEnum** | `0`: Pending, `1`: Approved, `2`: Rejected, `3`: Banned |
| **TeamDetailStatusEnum** | `0`: Pending, `1`: Active, `2`: Rejected |
| **SubmissionStatusEnum** | `0`: Submitted, `1`: Unsubmitted, `2`: Failed |
| **InvitationStatusEnum** | `0`: Pending, `1`: Accepted, `2`: Rejected, `3`: Expired |
| **EventRoleEnum** | `0`: Mentor, `1`: Judge, `2`: Staff |

---

## II. BẢNG TỔNG HỢP CHI TIẾT SỰ THAY ĐỔI CỦA CÁC API

> **Ghi chú**: Tất cả các vỏ bọc và trường dữ liệu phản hồi trả về từ API đều viết dưới định dạng **camelCase** (chữ cái đầu viết thường): `isSuccess`, `isFailed`, `status`, `error`, `traceId`, `timestampUtc`, `data`, `message`.

### 1. Nhóm Submissions (3 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **1** | `/api/v1/submissions/{submissionId}` | `GET` | Không đổi | `"status": "Submitted"`<br>`"message": "Bài chưa được chấm"` | `"status": 0`<br>`"message": "NOT_GRADED"` |
| **2** | `/api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}` | `GET` | **Mới hoàn toàn**<br>Query: `pageIndex`, `pageSize` | Không có (API mới) | Trả về `BasePaginationResponse`<br>`"status"` của bài nộp dạng số Enum. |
| **3** | `/api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}` | `POST` | **Mới hoàn toàn**<br>Body: `{"url": "...", "description": "..."}` | Không có (API mới) | Trả về `BaseResponse`<br>`"status"` dạng số Enum (`0`). |

---

### 2. Nhóm Profile & Auth (4 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **4** | `/api/v1/users/profile` | `GET` | Không đổi | Không trả ra trường `role` | Trả thêm `"role": 2` (Student) hoặc `3` (Lecturer) |
| **5** | `/api/v1/auth/me` | `GET` | Không đổi | `"role": "Student"` (Chuỗi) | `"role": 2` (Số nguyên) |
| **6** | `/api/v1/mentor/events` | `GET` | Không đổi | Trả thô trong `"Value"` và `role` là chuỗi. | Trả phân trang, `"role": 0` (Mentor) hoặc `1` (Judge). Trả toàn bộ event được gán. |
| **7** | `/api/v1/lecturers/events` | `GET` | **Mới hoàn toàn**<br>Query: `pageIndex`, `pageSize` (chỉ phân trang) | Không có (API mới) | Trả về `BasePaginationResponse`<br>`"role"` và `"eventStatus"` dạng số Enum. |

---

### 3. Nhóm Lời mời (Invitations) (3 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **8** | `/api/v1/invitations/me` | `GET` | Không đổi | `"status": "Pending"` (Chuỗi) | `"status": 0` (Số nguyên) |
| **9** | `/api/v1/invitations/{invitationId}/accept` | `POST` | Không đổi | `"status": "Accepted"` | `"status": 1` |
| **10**| `/api/v1/invitations/{invitationId}/reject` | `POST` | Không đổi | `"status": "Rejected"` | `"status": 2` |

---

### 4. Nhóm Đăng ký sự kiện (Register Teams) (2 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **11**| `/api/v1/register-teams/me` | `GET` | Không đổi | `"status": "Approved"` | `"status": 1` |
| **12**| `/api/v1/register-teams/{registerId}` | `GET` | Không đổi | `"status": "Approved"` | `"status": 1` |

---

### 5. Nhóm Đội thi (Teams) (3 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **13**| `/api/v1/teams/{teamId}` | `GET` | Không đổi | Trong `members`: `"status": "Active"` | Trong `members`: `"status": 1` |
| **14**| `/api/v1/teams/me` | `GET` | Không đổi | `"memberStatus": "Active"` | `"memberStatus": 1` |
| **15**| `/api/v1/teams` | `POST` | Không đổi | Trong `members`: `"status": "Active"` | Trong `members`: `"status": 1` |

---

### 6. Nhóm Vòng thi (Rounds) (3 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **16**| `/api/v1/rounds/{roundId}/submissions` | `GET` | Không đổi | Envelope camelCase | Đồng bộ Envelope camelCase |
| **17**| `/api/v1/rounds/{roundId}/my-submissions` | `GET` | Không đổi | `"status": 0`<br>Envelope camelCase | `"status": 0`<br>Đồng bộ Envelope camelCase |
| **18**| `/api/v1/rounds/{roundId}/scores/me` | `GET` | Không đổi | `"message": "Bài chưa được chấm"` | `"message": "NOT_GRADED"` |

---

### 7. Nhóm Sự kiện (Events) (6 API)

| # | Router chuẩn trên Controller | Method | Request cũ/mới | Response cũ | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **19**| `/api/v1/events` | `GET` | Không đổi | `"status": "Draft"` | `"status": 0` |
| **20**| `/api/v1/events/{eventId}` | `GET` | Không đổi | `"status": "Draft"` | `"status": 0` |
| **21**| `/api/v1/events/joined` | `GET` | Không đổi | `"status": "Draft"` | `"status": 0` |
| **22**| `/api/v1/events/most-participants` | `GET` | Không đổi | `"status": "Draft"` | `"status": 0` |
| **23**| `/api/v1/admin/events` | `GET` | Không đổi | `"status": "Draft"` | `"status": 0` |
| **24**| `/api/v1/admin/events/{eventId}/assignments` | `GET` | Không đổi | `"eventRoleName": "Judge"` | `"eventRoleName": 1` |

---

### 8. Nhóm Staff Gán Track & Topic (2 API)

*Đổi `eventId` và `teamId` truyền qua URL Route thay vì Body.*

| # | Router chuẩn trên Controller | Method | Request cũ | Request mới | Response mới |
| :--- | :--- | :---: | :--- | :--- | :--- |
| **25**| `/api/v1/staff/events/{eventId}/teams/{teamId}/track` | `PATCH` | Body: `{"trackId": "guid"}` | Body: `{"trackId": "guid"}` | Trả về `BaseResponse`<br>Chuẩn envelope camelCase. |
| **26**| `/api/v1/staff/events/{eventId}/teams/{teamId}/topic` | `PATCH` | Body: `{"topicId": "guid"}` | Body: `{"topicId": "guid"}` | Trả về `BaseResponse`<br>Chuẩn envelope camelCase. |

---

## III. QUY CHUẨN ĐỊNH DẠNG RESPONSE (ENVELOPE)

1. **Phản hồi Thành công (Success Response)**:
   Các trường root ngoài cùng sử dụng **camelCase**:
   `isSuccess`, `isFailed`, `status`, `error`, `traceId`, `timestampUtc`, `message`, `data`.
2. **Phản hồi Lỗi (Error Response từ Middleware)**:
   Trả về cấu trúc:
   `title`, `status`, `message` (chứa chi tiết lỗi), `messageCode` (mã snake_case), `errors`, `traceId`, `timestampUtc`.
