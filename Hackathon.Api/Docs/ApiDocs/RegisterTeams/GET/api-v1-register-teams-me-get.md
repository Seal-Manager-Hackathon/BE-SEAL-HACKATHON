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
| `status` | string | Không | Lọc theo trạng thái đơn đăng ký (Approved, Pending, Rejected). Nếu không truyền, mặc định trả về tất cả đơn đăng ký. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định 1) |
| `pageSize` | `int` | Không | Số lượng item trên mỗi trang (mặc định 10) |

## Ví dụ request
```http
GET /api/v1/register-teams/me?status=Approved&pageIndex=1&pageSize=10
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*

```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Data": {
    "Items": [
      {
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Chiến binh công nghệ",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "SEAL Hackathon 2026",
        "status": 1, /* 0: Pending, 1: Approved, 2: Rejected, 3: Banned */
        "description": "Dự án xe tự hành thông minh",
        "createdAt": "2026-06-19T10:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- Tài khoản phải là `Student` hợp lệ.
- User đang thuộc về những team nào (trạng thái member `Active` và team `IsDisable = false`), API sẽ tìm kiếm các đơn đăng ký của các team đó.
- Lọc theo điều kiện truyền vào `status` (ví dụ `status = Approved` để chỉ trả về các team **đã được staff duyệt chấp nhận**). Nếu không truyền `status`, mặc định trả về tất cả đơn đăng ký và được sắp xếp theo thứ tự trạng thái ưu tiên: `Pending` trước, đến `Approved`, rồi đến `Rejected`. Trong cùng một nhóm trạng thái hoặc khi lọc cụ thể, kết quả sắp xếp theo `CreatedAt` giảm dần.

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ Staff duyệt đơn |
| `1` | Approved | Đơn đăng ký đã được chấp nhận tham gia sự kiện |
| `2` | Rejected | Đơn đăng ký bị từ chối |
| `3` | Banned | Đội thi bị cấm thi đấu trong sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 400 | BAD_REQUEST | INVALID_STATUS |