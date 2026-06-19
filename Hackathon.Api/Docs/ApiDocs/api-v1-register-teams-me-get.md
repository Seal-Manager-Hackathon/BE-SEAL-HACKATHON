# Lấy danh sách event đã đăng ký (team đã được chấp nhận)

## Tác dụng
API dùng cho người dùng (Student) xem team của mình đã tham gia (đăng ký) vào những event nào. Chỉ lấy những event mà `RegisterTeam` có trạng thái là `Approved` (đã được chấp nhận).

## URL
`GET /api/v1/register-teams/me?status=Approved`

*(Lưu ý: API này dùng chung logic của `GetMyRegisteredEvents`, nếu chỉ truyền `status=Approved` thì sẽ trả về các event team đó tham gia đã được chấp nhận).*

## Authorization
Yêu cầu Access Token của `Student` (chỉ hiển thị những team mà user đó đang là member có `Status=Active` và không bị `IsDisable`).

## Path parameters
Không có

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `status` | string | Có | Truyền cứng `Approved` để lấy các team đã được duyệt. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định 1) |
| `pageSize` | `int` | Không | Số lượng item trên mỗi trang (mặc định 10) |

## Ví dụ request
```http
GET /api/v1/register-teams/me?status=Approved&pageIndex=1&pageSize=10
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "items": [
      {
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Tên team",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "Hackathon ABC",
        "status": 1 /* Approved */,
        "description": "Mô tả nếu có",
        "createdAt": "2026-06-19T10:00:00.0000000Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "totalPages": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Tài khoản phải là `Student` hợp lệ.
- User đang thuộc về những team nào (trạng thái member `Active` và team `IsDisable = false`), API sẽ tìm kiếm các đơn đăng ký của các team đó.
- Lọc theo điều kiện truyền vào `status = Approved` để chỉ trả về các team **đã được staff duyệt chấp nhận**.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 400 | BAD_REQUEST | INVALID_STATUS |