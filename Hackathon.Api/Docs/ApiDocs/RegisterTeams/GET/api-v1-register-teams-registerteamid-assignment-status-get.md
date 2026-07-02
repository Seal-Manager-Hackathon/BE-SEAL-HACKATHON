# Lấy trạng thái phân công Track & Topic của Register Team (Get Register Team Assignment Status)

## Tác dụng
Lấy thông tin đăng ký của đội bao gồm trạng thái duyệt, thông tin đội thi, sự kiện đăng ký và chi tiết Track & Topic được phân công. Nếu Track hoặc Topic chưa được phân công, giá trị trả về sẽ là `null` thay vì báo lỗi.

## URL
`GET /api/v1/register-teams/{registerTeamId}/assignment-status`

## Authorization
Yêu cầu truy cập của tài khoản đã đăng nhập (Authenticated).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerTeamId` | `guid` | Có | ID của đơn đăng ký team (`RegisterTeamId`). |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin chi tiết.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-03T06:30:00Z",
  "message": "SUCCESS",
  "data": {
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamName": "Đội tuyển siêu cấp",
    "eventId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "eventName": "SEAL Hackathon 2026",
    "status": "Approved",
    "isApproved": true,
    "trackId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "trackTitle": "Phát triển ứng dụng Web",
    "trackDescription": "Bảng đấu phát triển ứng dụng Web với công nghệ hiện đại",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Hệ thống quản lý điểm thi",
    "topicDescription": "Xây dựng hệ thống quản lý điểm cho các cuộc thi Hackathon"
  }
}
```

*Trong trường hợp chưa gán Track hoặc Topic:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-03T06:30:00Z",
  "message": "SUCCESS",
  "data": {
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamName": "Đội tuyển siêu cấp",
    "eventId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "eventName": "SEAL Hackathon 2026",
    "status": "Pending",
    "isApproved": false,
    "trackId": null,
    "trackTitle": null,
    "trackDescription": null,
    "topicId": null,
    "topicTitle": null,
    "topicDescription": null
  }
}
```

## Lỗi có thể xảy ra
* Trả về lỗi `404 Not Found` nếu không tìm thấy đơn đăng ký đội thi ứng với `registerTeamId`.
