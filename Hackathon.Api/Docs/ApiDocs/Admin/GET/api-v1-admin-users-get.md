# Admin lấy danh sách user

## Tác dụng
Admin lấy danh sách tất cả người dùng (Users), hỗ trợ lọc theo role và có phân trang.

## URL
`GET /api/v1/admin/users`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
Không có.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `role` | `int` | Không | Lọc theo role người dùng. Xem bảng `RoleEnum` bên dưới. Nếu không truyền, trả về tất cả role. |
| `keyword` | `string` | Không | Tìm kiếm theo tên (firstName/lastName, Contains). Nếu nhập email, dùng `mailSearch` ở API search. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`, tối đa `100`). |

## Ví dụ request
```http
GET /api/v1/admin/users?role=2&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
Response dùng `ApiResponseFactory.BasePagination(...)`.
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "id": "guid",
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "phoneNumber": "string",
        "avatarUrl": "string",
        "studentId": "string | null",
        "college": "string | null",
        "role": 2 /* RoleEnum: 0=Admin, 1=Staff, 2=Student, 3=Lecturer */,
        "status": 1 /* UserStatusEnum */,
        "isVerified": true,
        "isDisable": false,
        "createdAt": "datetimeoffset"
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

### Bảng RoleEnum (SystemRole)
| Giá trị (Value) | Vai trò (Role) | Mô tả |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên quản lý |
| `2` | Student | Sinh viên/Thí sinh |
| `3` | Lecturer | Giảng viên |

### Bảng UserStatusEnum
| Giá trị (Value) | Trạng thái | Mô tả |
| :--- | :--- | :--- |
| `0` | Inactive | Không hoạt động |
| `1` | Active | Đang hoạt động |

## Business rules
- Người gọi phải có role `Admin`.
- Nếu không truyền `role`, trả về tất cả user đang hoạt động ở mọi role.
- Nếu truyền `role`, chỉ trả về user đang hoạt động có role tương ứng (`0=Admin`, `1=Staff`, `2=Student`, `3=Lecturer`).
- Không trả về user đã bị ban/soft-disable (`IsDisable = true`).
- Nếu cần xem/lọc user bị soft-disable, dùng `GET /api/v1/admin/users/search` với param `isDisable`.
- Sắp xếp theo `CreatedAt` giảm dần (mới nhất trước).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 400 | BAD_REQUEST | INVALID_ROLE |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.AdminController`.
- Route: `GET /api/v1/admin/users`.
- Sử dụng policy `AdminPolicy`.
