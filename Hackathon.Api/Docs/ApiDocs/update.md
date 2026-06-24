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
