# Student xem chi tiết team

## Tác dụng
Lấy thông tin chi tiết của một team, bao gồm các thành viên với chi tiết như Tên, Ngày sinh, MSSV, Trường học.

## URL
`GET /api/v1/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Student`, `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần xem. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/teams/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": {
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "name": "Chiến binh công nghệ",
    "canEdit": true,
    "isLeader": true,
    "createdAt": "2026-06-22T08:00:00Z",
    "members": [
      {
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "firstName": "Nguyễn Văn",
        "lastName": "A",
        "dateOfBirth": "2000-01-01T00:00:00Z",
        "studentId": "STU123456",
        "college": "FPT University",
        "isLeader": true,
        "status": 1 /* 0: Pending, 1: Active, 2: Rejected */
      }
    ]
  }
}
```

## Business rules
- Chỉ hiển thị nếu người dùng là thành viên team hoặc Staff/Admin.

### Bảng trạng thái thành viên TeamDetailStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang đợi trưởng nhóm duyệt vào |
| `1` | Active | Thành viên chính thức hoạt động |
| `2` | Rejected | Yêu cầu tham gia bị từ chối |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | TEAM_NOT_VISIBLE_TO_USER |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
