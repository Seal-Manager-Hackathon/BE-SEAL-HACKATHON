# Staff xem danh sách phân công trong Event

## Tác dụng

Staff hoặc Admin lấy danh sách phân công (`AssignEvents`) trong sự kiện, bao gồm các vai trò `Mentor`, `Judge`. Có hỗ trợ lọc theo vai trò (EventRoleEnum), tìm kiếm tên/email, lọc theo trackId, lọc soft-disable và phân trang.

**Phân biệt quyền:**

- **Admin:** Xem được tất cả (Staff + Lecturer) trong event.
- **Staff:** Chỉ xem được Lecturer (không thấy Staff khác).

## URL

`GET /api/v1/staff/events/{eventId}/assignments`

## Authorization

Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters

| Tên       | Kiểu dữ liệu | Bắt buộc |           Mô tả |
| --------- | ------------ | -------- | --------------: |
| `eventId` | `guid`       | Có       | Id của sự kiện. |

## Query parameters

| Tên         | Kiểu dữ liệu | Bắt buộc |                                                                                       Mô tả |
| ----------- | ------------ | -------- | ------------------------------------------------------------------------------------------: |
| `eventRole` | `int`        | Không    | Lọc theo vai trò trong sự kiện (EventRoleEnum). Nếu không truyền (hoặc `null`), lấy tất cả. |
| `keyword`   | `string`     | Không    |                                     Từ khóa tìm kiếm theo tên hoặc email của user được gán. |
| `trackId`   | `guid`       | Không    |              Lọc những người được phân công vào track cụ thể. Nếu không truyền, lấy tất cả. |
| `isDisable` | `bool`       | Không    |                           Lọc theo trạng thái soft-disable của phân công. Mặc định `false`. |
| `pageIndex` | `int`        | Không    |                                                              Trang hiện tại (mặc định `1`). |
| `pageSize`  | `int`        | Không    |                                                          Số item mỗi trang (mặc định `10`). |

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
        "id": "guid" /* ID của AssignEvent */,
        "userId": "guid" /* ID của User */,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "eventRoleId": "guid",
        "eventRole": 1 /* EventRoleEnum */,
        "role": 3 /* RoleEnum */,
        "isDisable": false,
        "createdAt": "datetimeoffset",
        "assignedTracks": [
          {
            "assignTrackId": "guid",
            "trackId": "guid",
            "trackTitle": "string",
            "isDisable": false
          }
        ]
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

### Bảng vai trò EventRoleEnum (Integer)

| Giá trị (Value) | Vai trò (Role) | Mô tả (Description)                    |
| :-------------- | :------------- | :------------------------------------- |
| `0`             | Mentor         | Người hướng dẫn chuyên môn cho đội thi |
| `1`             | Judge          | Giám khảo chấm điểm bài thi            |
| `2`             | Staff          | Nhân viên vận hành sự kiện             |

### Bảng vai trò hệ thống RoleEnum (SystemRole)

| Giá trị (Value) | Vai trò (Role) | Mô tả                  |
| :-------------- | :------------- | :--------------------- |
| `0`             | Admin          | Quản trị viên hệ thống |
| `1`             | Staff          | Nhân viên quản lý      |
| `2`             | Student        | Sinh viên/Thí sinh     |
| `3`             | Lecturer       | Giảng viên             |

## Business rules

- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện này trước (`AssignEvents`), nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu là Admin: không cần kiểm tra phân công quản lý sự kiện.
- `eventId` phải tồn tại và không bị disable.
- Lọc theo `eventRole` nếu được truyền vào.
- Lọc theo `trackId` nếu được truyền vào — trả về những lecturer có AssignTrack thuộc track đó.
- **Phân biệt quyền:** Admin thấy tất cả (Staff + Lecturer); Staff chỉ thấy Lecturer.
- Trả về `id` chính là `assignEventId` (ID của record AssignEvents).
- Trả về `eventRoleId` là ID của EventRole (Mentor/Judge/Staff).
- Mỗi item trả về kèm danh sách `assignedTracks` — các track mà lecturer (Judge/Mentor) được phân công trong event này (chỉ lấy track chưa bị soft-disable). Mỗi track có `assignTrackId` riêng.
- Trả về cả `role` (role ngoài cùng của User) và `eventRole` (role trong event) riêng biệt.

## Lỗi có thể xảy ra

| HTTP | messageCode           | message/detail               |
| ---: | --------------------- | ---------------------------- |
|  401 | MISSING_ACCESS_TOKEN  | ACCESS_TOKEN_IS_MISSING      |
|  401 | UNAUTHORIZED          | INVALID_ACCESS_TOKEN         |
|  403 | FORBIDDEN             | FORBIDDEN                    |
|  403 | FORBIDDEN             | STAFF_NOT_ASSIGNED_TO_EVENT  |
|  404 | NOT_FOUND             | EVENT_NOT_FOUND              |
|  404 | NOT_FOUND             | NO_ONE_ASSIGNED_TO_EVENT     |
|  500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
