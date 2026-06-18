# Tạo báo cáo hệ thống (System Report)

## Tác dụng
Cho phép người dùng tạo báo cáo (System Report) gửi lên hệ thống. Báo cáo có thể liên quan đến một Assignment hoặc một Submission.

## URL
`POST /api/users/system-report`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
Không có.

## Query parameters
Không có.

## Ví dụ request
```http
POST /api/users/system-report
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "assignEventId": "00000000-0000-0000-0000-000000000000",
    "submissionId": "00000000-0000-0000-0000-000000000000",
    "title": "Báo cáo vấn đề X",
    "description": "Chi tiết vấn đề X",
    "imgUrl": "https://example.com/evidence.jpg",
    "fileUrl": "https://example.com/evidence.pdf",
    "typeReport": "Lỗi hệ thống"
}
```

## Request body
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `assignEventId` | `guid` | Có | Id của sự kiện/track được phân công (AssignEvent). |
| `submissionId` | `guid` | Có | Id của bài nộp (Submission) liên quan. |
| `title` | `string` | Có | Tiêu đề báo cáo. |
| `description` | `string` | Có | Mô tả chi tiết vấn đề. |
| `imgUrl` | `string` | Không | Link hình ảnh minh chứng. |
| `fileUrl` | `string` | Không | Link file minh chứng. |
| `typeReport` | `string` | Không | Loại báo cáo. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "2026-06-18T23:00:00Z",
  "value": "Gửi báo cáo thành công"
}
```

## Business rules
- Khi người dùng tạo báo cáo, ID người tạo (`userId`) tự động lấy từ access token.
- Báo cáo tạo ra sẽ có trạng thái mặc định là `Open`.
- `createdAt` và `updatedAt` được tự động ghi nhận tại thời điểm gửi.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Lỗi validation từ định dạng Guid hoặc Request Body. |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing or invalid. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
