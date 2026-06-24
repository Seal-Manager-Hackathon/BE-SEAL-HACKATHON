# Đặc Tả Thiết Kế Module Lecturers (Lecturer Events API)

Tài liệu này đặc tả thiết kế chi tiết cho module mới `Lecturers` hỗ trợ giảng viên lấy danh sách các sự kiện được phân công.

---

## 1. Thiết Kế API Endpoint

### **Router chuẩn trên Controller:**
`GET /api/v1/lecturers/events`

### **Authorization:**
Yêu cầu Access Token hợp lệ của Giảng viên (`role = 3` tương ứng với `RoleEnum.Lecturer`).

### **Request Parameters (Query):**
- `pageIndex` (int, Không bắt buộc, mặc định 1): Số trang cần lấy.
- `pageSize` (int, Không bắt buộc, mặc định 10): Số phần tử trên mỗi trang (giới hạn tối đa 100).

### **Response Body (Success - 200 OK):**
*Cấu trúc trả về dạng `BasePaginationResponse` với các trường camelCase:*
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
        "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "eventName": "SEAL Hackathon 2026",
        "season": "Mùa hè 2026",
        "startTime": "2026-07-01T08:00:00Z",
        "endTime": "2026-07-10T17:00:00Z",
        "role": 0, /* 0: Mentor, 1: Judge */
        "eventStatus": 1 /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
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

---

## 2. Phân Tích Cấu Trúc Mã Nguồn

### **Tầng Request/Response DTOs**:
- Đường dẫn: `Hackathon.Service/Lecturers/Response.cs`.
- Request DTO dùng trực tiếp `PaginationRequest` (không có bộ lọc thêm).
- Response DTO chứa các thông tin cơ bản của Event lấy từ thực thể `Events` và vai trò `EventRole` tương ứng.

### **Tầng Validators**:
- Đường dẫn: `Hackathon.Service/Validations/Lecturers/GetLecturerEventsRequestValidator.cs`.
- validator sử dụng thư viện `FluentValidation` kế thừa `AbstractValidator<Request.GetLecturerEventsRequest>` để validate `PageIndex` và `PageSize`.

### **Tầng Service**:
- Đường dẫn: `Hackathon.Service/Lecturers/IService.cs` và `Service.cs`.
- Logic:
  1. Kiểm tra xác thực của Giảng viên qua Access Token.
  2. Parse claim role của user sang `RoleEnum` để so sánh trực tiếp, ném `ForbiddenException("FORBIDDEN")` nếu không phải giảng viên.
  3. Query các bản ghi từ bảng nối `AssignEvents` liên kết với `Events` và `EventRoles`.
  4. Lọc theo `Keyword` (tìm kiếm gần đúng không phân biệt chữ hoa thường trên `Event.Name` hoặc `Event.Season`).
  5. Lọc theo `Role` (`EventRole.Name == request.Role`).
  6. Sắp xếp kết quả giảm dần theo thời gian phân công hoặc thời gian tạo sự kiện (`AssignEvents.CreatedAt` giảm dần).
  7. Phân trang sử dụng `.Skip((pageIndex - 1) * pageSize).Take(pageSize)`.
  8. Trả về kết quả bọc qua `ApiResponseFactory.BasePagination(...)`.

### **Tầng Controller**:
- Đường dẫn: `Hackathon.Api/Controllers/LecturersController.cs`.
- Route: `api/v1/lecturers`.
- Action: `[HttpGet("events")]` gọi phương thức của `LecturersService.IService`.

### **Cấu Hình Program.cs**:
- Đăng ký DI Scoped cho `LecturersService`.
