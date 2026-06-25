# Nhật ký Cập nhật API và Codebase (update.md)

Tài liệu này tổng hợp toàn bộ các thay đổi về API, cấu trúc code Service/Response DTO và tài liệu đặc tả API tương ứng đã được thực hiện để chuẩn hóa hệ thống.

---

## 1. Thêm Endpoint và Chuyển đổi Validation cho Submissions

### Thay đổi trong Code:
- **Thêm Endpoint trong Controller**:
  - Bổ sung `GET /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}` để lấy danh sách bài nộp theo round & register team.
  - Bổ sung `POST /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}` để nộp dự án.
  - *Đường dẫn:* [SubmissionsController.cs](Hackathon.Api/Controllers/SubmissionsController.cs)
- **Tách lọc / chuẩn hóa Validation sang FluentValidation**:
  - Di chuyển toàn bộ DataAnnotations (`[Required]`, `[Url]`, `[Range]`) khỏi request DTO của Submissions tại [Request.cs](Hackathon.Service/Submissions/Request.cs).
  - Tạo mới các validator FluentValidation tương ứng tại [SubmitRoundProjectRequestValidator.cs](Hackathon.Service/Validations/Submissions/SubmitRoundProjectRequestValidator.cs) và [GetSubmissionsRequestValidator.cs](Hackathon.Service/Validations/Submissions/GetSubmissionsRequestValidator.cs).
  - Loại bỏ các đoạn code normalize/validate page size và index thủ công tại [Service.cs](Hackathon.Service/Submissions/Service.cs).
- **Chuẩn hóa thông điệp lỗi**:
  - Chuyển thông điệp `"Bài chưa được chấm"` thành `"NOT_GRADED"` ở tầng service.

### Thay đổi trong Tài liệu (Docs):
- Cập nhật mock response thành PascalCase và thay thế message `"Bài chưa được chấm"` thành `"NOT_GRADED"` tại [api-v1-submissions-id-get.md](Hackathon.Api/Docs/ApiDocs/Submissions/GET/api-v1-submissions-id-get.md).
- Thêm tài liệu mới cho endpoint GET list bài nộp tại [api-v1-submissions-rounds-id-register-teams-id-get.md](Hackathon.Api/Docs/ApiDocs/Submissions/GET/api-v1-submissions-rounds-id-register-teams-id-get.md).
- Cập nhật tài liệu nộp bài tại [api-v1-rounds-id-register-teams-id-submissions-post.md](Hackathon.Api/Docs/ApiDocs/Submissions/POST/api-v1-rounds-id-register-teams-id-submissions-post.md) (khớp mã lỗi FluentValidation: `URL_REQUIRED`, `INVALID_URL_FORMAT` và sửa mock response).

---

## 2. Chuẩn hóa Trả về Enum dạng Số nguyên (Integer) thay vì String

Để Frontend (FE) dễ dàng xử lý, toàn bộ các trường `Status`, `Role`, `EventRoleName`, `SubmissionStatus` và `MemberStatus` được chuyển từ kiểu chuỗi `string` (nhận từ `.ToString()`) thành kiểu **Enum số nguyên (Integer)** gốc.

### Thay đổi trong Code:
- **Module `Users` (Profile)**:
  - Thêm trường `Role` kiểu `RoleEnum` vào [Users/Response.cs](Hackathon.Service/Users/Response.cs).
  - Ánh xạ `Role = user.Role` trong [Users/Service.cs](Hackathon.Service/Users/Service.cs).
- **Module `Events`**:
  - Đổi kiểu `Status` trong DTOs `EventResponse`, `StudentEventResponse`, `AdminEventResponse` thành `EventStatusEnum?`.
  - Đổi kiểu `EventRoleName` trong `EventAssignmentResponse` thành `EventRoleEnum?`.
  - Loại bỏ các hàm gọi `.ToString()` khi gán thuộc tính tương ứng ở [Events/Service.cs](Hackathon.Service/Events/Service.cs).
- **Module `RegisterTeams`**:
  - Đổi kiểu `Status` trong `RegisterTeamDetailForStudentResponse` và `RegisteredEventItemResponse` thành `RegisterTeamStatusEnum?`.
  - Loại bỏ `.ToString()!` trong [RegisterTeams/Service.cs](Hackathon.Service/RegisterTeams/Service.cs).
- **Module `Invitations`**:
  - Đổi kiểu `Status` trong `InvitationItemResponse` thành `InvitationStatusEnum?`.
  - Loại bỏ `.ToString()` trong [Invitations/Service.cs](Hackathon.Service/Invitations/Service.cs).
- **Module `Teams`**:
  - Đổi kiểu `Status`/`MemberStatus` thành `TeamDetailStatusEnum?` và kiểu `Status` đăng ký thành `RegisterTeamStatusEnum?` trong [Teams/Response.cs](Hackathon.Service/Teams/Response.cs).
  - Loại bỏ `.ToString()` trong [Teams/Service.cs](Hackathon.Service/Teams/Service.cs).
- **Module `AssignEvents`**:
  - Đổi kiểu `EventRoleName` trong `AssignLecturerDetailResponse` thành `EventRoleEnum?`.
  - Loại bỏ `.ToString()` tại [AssignEvents/Service.cs](Hackathon.Service/AssignEvents/Service.cs).
- **Module `Rounds`**:
  - Đổi kiểu `Status` và `SubmissionStatus` trong các DTOs thành `SubmissionStatusEnum?`.
  - Loại bỏ `.ToString()` và cast `(int)` khi ánh xạ tại [Rounds/Service.cs](Hackathon.Service/Rounds/Service.cs).

### Thay đổi trong Tài liệu (Docs):
Đã cập nhật các giá trị mock JSON response về dạng số nguyên và thêm bảng giải thích các giá trị Enum cụ thể (0, 1, 2, 3...) tại:
- [api-v1-users-me-profile-get.md](Hackathon.Api/Docs/ApiDocs/Users/GET/api-v1-users-me-profile-get.md) (`role`: 2, thêm bảng `RoleEnum`)
- [api-v1-auth-me.md](Hackathon.Api/Docs/ApiDocs/Auth/GET/api-v1-auth-me.md) (`role`: 2, thêm bảng `RoleEnum`)
- [api-v1-admin-events-id-assignments-get.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-admin-events-id-assignments-get.md) (`eventRoleName`: 1)
- Các file GET Events: [api-v1-admin-events-get.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-admin-events-get.md), [api-v1-events-get.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-events-get.md), [api-v1-events-id-get.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-events-id-get.md), [api-v1-events-joined-get.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-events-joined-get.md), [api-v1-events-most-participants.md](Hackathon.Api/Docs/ApiDocs/Events/GET/api-v1-events-most-participants.md).
- Các file GET Lời mời: [api-v1-invitations-me-get.md](Hackathon.Api/Docs/ApiDocs/Invitations/GET/api-v1-invitations-me-get.md), [api-v1-invitations-id-accept-post.md](Hackathon.Api/Docs/ApiDocs/Invitations/POST/api-v1-invitations-id-accept-post.md), [api-v1-invitations-id-reject-post.md](Hackathon.Api/Docs/ApiDocs/Invitations/POST/api-v1-invitations-id-reject-post.md).
- Các file GET Đăng ký Event: [api-v1-register-teams-me-get.md](Hackathon.Api/Docs/ApiDocs/RegisterTeams/GET/api-v1-register-teams-me-get.md), [api-v1-register-teams-id-get.md](Hackathon.Api/Docs/ApiDocs/RegisterTeams/GET/api-v1-register-teams-id-get.md).
- Các file GET Rounds: [api-v1-rounds-id-my-submissions-get.md](Hackathon.Api/Docs/ApiDocs/Rounds/GET/api-v1-rounds-id-my-submissions-get.md), [api-v1-rounds-id-submissions-get.md](Hackathon.Api/Docs/ApiDocs/Rounds/GET/api-v1-rounds-id-submissions-get.md), [api-v1-rounds-id-scores-me-get.md](Hackathon.Api/Docs/ApiDocs/Rounds/GET/api-v1-rounds-id-scores-me-get.md).
- Các file GET Teams: [api-v1-teams-id-get.md](Hackathon.Api/Docs/ApiDocs/Teams/GET/api-v1-teams-id-get.md), [api-v1-teams-me-get.md](Hackathon.Api/Docs/ApiDocs/Teams/GET/api-v1-teams-me-get.md), [api-v1-teams-post.md](Hackathon.Api/Docs/ApiDocs/Teams/POST/api-v1-teams-post.md).

---

## 3. Chuyển đổi So sánh Vai trò từ String sang Enum trong C#

Để đảm bảo hiệu năng và tính nhất quán, toàn bộ các logic kiểm tra vai trò người dùng (Staff, Admin, Lecturer,...) dạng chuỗi được chuyển sang so sánh **Enum trực tiếp**.

### Thay đổi trong Code:
- Loại bỏ kiểm tra bằng hàm `IsInRole(RoleEnum.X.ToString())` (ở ASP.NET Core) tại các file Service:
  - [AssignTracks/Service.cs](Hackathon.Service/AssignTracks/Service.cs)
  - [LeaderBoards/Service.cs](Hackathon.Service/LeaderBoards/Service.cs)
  - [Events/Service.cs](Hackathon.Service/Events/Service.cs)
  - [Tracks/Service.cs](Hackathon.Service/Tracks/Service.cs)
  - [AssignEvents/Service.cs](Hackathon.Service/AssignEvents/Service.cs)
  - [Teams/Service.cs](Hackathon.Service/Teams/Service.cs)
  - [Rounds/Service.cs](Hackathon.Service/Rounds/Service.cs)
  - [RegisterTeams/Service.cs](Hackathon.Service/RegisterTeams/Service.cs)
- Thay thế bằng việc lấy trực tiếp claim `Role`, parse sang `RoleEnum` (sử dụng `Enum.TryParse<RoleEnum>`) và so sánh Enum trực tiếp.
- Sửa đổi các hàm `EnsureCanViewSubmission` và `GetSubmissions` tại [Submissions/Service.cs](Hackathon.Service/Submissions/Service.cs).

---

## 4. Chuẩn hóa Hướng dẫn Tiêu chuẩn API

Đã cập nhật quy định lập trình và thiết kế API vào các file skill hướng dẫn:
- **`create-api-skill/SKILL.md`**: Thiết lập FluentValidation là bắt buộc thay thế DataAnnotations trên Request DTO, đặt `ChangePasswordRequestValidator` làm mẫu. Bổ sung quy tắc PascalCase cho Response Envelope và bảng giải thích lỗi đồng bộ với exception C#.
- **`verifying-api-standards/SKILL.md`**: Cập nhật quy tắc Audit kiểm tra FluentValidation thay thế DataAnnotations, kiểm tra PascalCase Response Envelope và bảng lỗi đồng bộ.
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
- `2` $\rightarrow$ **Staff** (Nhân viên vận hành)

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
