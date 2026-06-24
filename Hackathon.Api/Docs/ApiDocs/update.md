# Hướng Dẫn Cập Nhật API Chi Tiết Cho Frontend AI (update.md)

Tài liệu này cung cấp chi tiết tất cả những thay đổi về Endpoint, Cấu trúc Request/Response JSON (cũ vs mới) và các bảng mã Enum số nguyên để AI phát triển Frontend có thể đọc hiểu và cập nhật code giao diện, xử lý dữ liệu chính xác mà không cần xem mã nguồn Backend.

---

## I. Danh Sách Các Bảng Mã Enum Số Nguyên (Mới)

Thay vì hứng dữ liệu dạng chuỗi như trước, Frontend cần đổi toàn bộ logic đối chiếu sang các số nguyên tương ứng dưới đây:

### 1. Vai trò của Tài khoản (RoleEnum)
- `0` $\rightarrow$ **Admin** (Quản trị viên)
- `1` $\rightarrow$ **Staff** (Nhân viên vận hành)
- `2` $\rightarrow$ **Student** (Thí sinh / Sinh viên)
- `3` $\rightarrow$ **Lecturer** (Giảng viên hỗ trợ / Chấm thi)

### 2. Trạng thái Sự kiện (EventStatusEnum)
- `0` $\rightarrow$ **Draft** (Bản nháp)
- `1` $\rightarrow$ **Published** (Đang diễn ra / Đã công bố)
- `2` $\rightarrow$ **Closed** (Đã đóng)
- `3` $\rightarrow$ **Cancelled** (Đã hủy bỏ)

### 3. Trạng thái Đăng ký Đội thi (RegisterTeamStatusEnum)
- `0` $\rightarrow$ **Pending** (Chờ duyệt)
- `1` $\rightarrow$ **Approved** (Đã duyệt tham gia)
- `2` $\rightarrow$ **Rejected** (Bị từ chối)
- `3` $\rightarrow$ **Banned** (Bị cấm thi đấu)

### 4. Trạng thái thành viên trong Đội (TeamDetailStatusEnum)
- `0` $\rightarrow$ **Pending** (Chờ duyệt vào đội)
- `1` $\rightarrow$ **Active** (Thành viên chính thức)
- `2` $\rightarrow$ **Rejected** (BTC từ chối duyệt)

### 5. Trạng thái bài nộp (SubmissionStatusEnum)
- `0` $\rightarrow$ **Submitted** (Đã nộp bài thành công)
- `1` $\rightarrow$ **Unsubmitted** (Chưa nộp bài / Đang soạn)
- `2` $\rightarrow$ **Failed** (Nộp bài thất bại)

### 6. Trạng thái lời mời vào Đội (InvitationStatusEnum)
- `0` $\rightarrow$ **Pending** (Chờ người dùng phản hồi)
- `1` $\rightarrow$ **Accepted** (Đã chấp nhận)
- `2` $\rightarrow$ **Rejected** (Đã từ chối)
- `3` $\rightarrow$ **Expired** (Lời mời hết hạn)

### 7. Vai trò trong Sự kiện (EventRoleEnum)
- `0` $\rightarrow$ **Mentor** (Người hướng dẫn chuyên môn)
- `1` $\rightarrow$ **Judge** (Ban giám khảo)

---

## II. Quy Chuẩn Định Dạng Response (camelCase)

Tất cả các API trong hệ thống đều trả về cấu trúc JSON định dạng **camelCase** (chữ cái đầu viết thường) cho cả vỏ bọc ngoài cùng (`ApiResponse`) và dữ liệu thực tế bên trong.

### 1. Phản hồi Thành công (Success Response)
- **Response dạng camelCase**:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "message": "SUCCESS",
    "data": { ... }
  }
  ```

### 2. Phản hồi Lỗi (Error Response từ Middleware)
Khi xảy ra lỗi (400, 401, 403, 404, 500), API trả về cấu trúc lỗi phẳng của Middleware:
- **Response dạng camelCase**:
  ```json
  {
    "title": "Forbidden",
    "status": 403,
    "message": "Mã lỗi viết hoa hoặc chi tiết lỗi",
    "messageCode": "FORBIDDEN",
    "errors": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z"
  }
  ```

---

## III. Chi Tiết Thay Đổi Trên Từng API Router

Dưới đây là chi tiết so sánh cấu trúc response cũ vs mới của các API bị ảnh hưởng.

### 1. GET /api/v1/submissions/{submissionId}
- **Response CŨ**: `"status": "Submitted"` (chuỗi), `"message": "Bài chưa được chấm"`.
- **Response MỚI**:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "message": "SUCCESS",
    "data": {
      "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
      "status": 0, // Số nguyên Enum (0 = Submitted)
      "gradingStatus": "NotGraded",
      "message": "NOT_GRADED"
    }
  }
  ```

### 2. GET /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId} (API mới)
- **Response MỚI** (Phân trang dạng camelCase, status dạng số):
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "data": {
      "items": [
        {
          "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
          "url": "https://github.com/project",
          "description": "Bài làm",
          "status": 0, // Số nguyên (0 = Submitted)
          "submittedAt": "2026-06-22T08:00:00Z"
        }
      ],
      "pageIndex": 1,
      "pageSize": 10,
      "totalCount": 1,
      "hasNextPage": false,
      "hasPreviousPage": false
    }
  }
  ```

### 3. POST /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId} (API mới)
- **Response MỚI**:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "message": "SUBMISSION_CREATED_SUCCESSFULLY",
    "data": {
      "submissionId": "f9b8c7d6-e5a4-3210-9c0d-1e2f3a4b5c6d",
      "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "submittedAt": "2026-06-22T08:00:00Z",
      "status": 0, // Số nguyên
      "isSuccess": true
    }
  }
  ```

### 4. GET /api/v1/users/profile
- **Response MỚI**: Bổ sung trường `role`: `2` (Student) hoặc `3` (Lecturer).

### 5. GET /api/v1/auth/me
- **Response CŨ**: `"role": "Student"`
- **Response MỚI**: `"role": 2` (Số nguyên)

### 6. GET /api/v1/mentor/events
- **Response MỚI**: Trả về phân trang, `role` dạng số nguyên `0` (Mentor) hoặc `1` (Judge). Lấy tất cả sự kiện phân công.

### 7. GET /api/v1/invitations/me
- **Response CŨ**: `"status": "Pending"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 8. POST /api/v1/invitations/{invitationId}/accept
- **Response CŨ**: `"status": "Accepted"`
- **Response MỚI**: `"status": 1` (Số nguyên)

### 9. POST /api/v1/invitations/{invitationId}/reject
- **Response CŨ**: `"status": "Rejected"`
- **Response MỚI**: `"status": 2` (Số nguyên)

### 10. GET /api/v1/register-teams/me
- **Response CŨ**: `"status": "Approved"`
- **Response MỚI**: `"status": 1` (Số nguyên)

### 11. GET /api/v1/register-teams/{registerId}
- **Response CŨ**: `"status": "Approved"`
- **Response MỚI**: `"status": 1` (Số nguyên)

### 12. GET /api/v1/teams/{teamId}
- **Response CŨ**: Trong mảng `members`, trường `"status": "Active"`
- **Response MỚI**: Trong mảng `members`, trường `"status": 1` (Số nguyên)

### 13. GET /api/v1/teams/me
- **Response CŨ**: `"memberStatus": "Active"`
- **Response MỚI**: `"memberStatus": 1` (Số nguyên)

### 14. POST /api/v1/teams
- **Response CŨ**: Trong mảng `members`, trường `"status": "Active"`
- **Response MỚI**: Trong mảng `members`, trường `"status": 1` (Số nguyên)

### 15. GET /api/v1/rounds/{roundId}/submissions
- **Response MỚI**: Đồng bộ envelope thành camelCase, `status` giữ nguyên `0`.

### 16. GET /api/v1/rounds/{roundId}/my-submissions
- **Response CŨ**: `"status": 0` (Thành công nộp)
- **Response MỚI**: `"status": 0`, đồng bộ envelope thành camelCase.

### 17. GET /api/v1/rounds/{roundId}/scores/me
- **Response CŨ**: `"message": "Bài chưa được chấm"`.
- **Response MỚI**: `"message": "NOT_GRADED"`, sửa envelope thành camelCase.

### 18. GET /api/v1/events
- **Response CŨ**: `"status": "Draft"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 19. GET /api/v1/events/{eventId}
- **Response CŨ**: `"status": "Draft"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 20. GET /api/v1/events/joined
- **Response CŨ**: `"status": "Draft"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 21. GET /api/v1/events/most-participants
- **Response CŨ**: `"status": "Draft"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 22. GET /api/v1/admin/events
- **Response CŨ**: `"status": "Draft"`
- **Response MỚI**: `"status": 0` (Số nguyên)

### 23. GET /api/v1/admin/events/{eventId}/assignments
- **Response CŨ**: `"eventRoleName": "Judge"`
- **Response MỚI**: `"eventRoleName": 1` (Số nguyên)

### 24. GET /api/v1/lecturers/events (API mới hoàn toàn)
- **Tác dụng**: Giảng viên lấy danh sách các sự kiện mình được phân công.
- **Request**: Query params: `pageIndex`, `pageSize` (chỉ có phân trang chuẩn, không có bộ lọc thêm).
- **Response MỚI**: Trả về `BasePaginationResponse` dạng camelCase, trường `role` dạng số Enum (`0 = Mentor, 1 = Judge`), `eventStatus` dạng số Enum.

---

## IV. API Gán Track & Topic Của Staff (Truyền EventId qua URL Route)

### 1. PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/track
- **Đường dẫn CŨ**: `PATCH /api/v1/staff/teams/{teamId}/track`
- **Đường dẫn MỚI**: `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/track`
- **Request Body**:
  ```json
  {
    "trackId": "guid"
  }
  ```
- **Response MỚI (camelCase)**:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "message": "TRACK_ASSIGNED_TO_TEAM_SUCCESSFULLY",
    "data": {
      "teamId": "guid",
      "teamName": "string",
      "eventId": "guid",
      "trackId": "guid",
      "trackTitle": "string"
    }
  }
  ```

### 2. PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/topic
- **Đường dẫn CŨ**: `PATCH /api/v1/staff/teams/{teamId}/topic`
- **Đường dẫn MỚI**: `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/topic`
- **Request Body**:
  ```json
  {
    "topicId": "guid"
  }
  ```
- **Response MỚI (camelCase)**:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "message": "TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY",
    "data": {
      "teamId": "guid",
      "teamName": "string",
      "eventId": "guid",
      "trackId": "guid",
      "trackTitle": "string",
      "topicId": "guid",
      "topicTitle": "string"
    }
  }
}
```
