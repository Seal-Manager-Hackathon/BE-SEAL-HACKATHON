# Lấy thông tin enums hệ thống (Get System Enums)

## Tác dụng
Lấy danh sách giá trị và ý nghĩa của toàn bộ enum dùng chung trong hệ thống để FE đồng bộ hiển thị, lọc dữ liệu và mapping status/code mà không hard-code rời rạc.

## URL
`GET /api/v1/enums`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa metadata các enum.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "GET_SYSTEM_ENUMS_SUCCESSFUL",
  "data": {
    "EmailVerificationStatusEnum": {
      "0": "Pending",
      "1": "Verified",
      "2": "Expired"
    },
    "EventRoleEnum": {
      "0": "Mentor",
      "1": "Judge",
      "2": "Staff"
    },
    "EventStatusEnum": {
      "0": "Draft",
      "1": "Published",
      "2": "Closed"
    },
    "InvitationStatusEnum": {
      "0": "Pending",
      "1": "Accepted",
      "2": "Rejected",
      "3": "Expired"
    },
    "LeaderBoardsStatusEnum": {
      "0": "IsDisabled"
    },
    "NotificationStatusEnum": {
      "0": "Pending",
      "1": "Unread",
      "2": "Read"
    },
    "RegisterTeamStatusEnum": {
      "0": "Pending",
      "1": "Approved",
      "2": "Rejected"
    },
    "ReportStatusEnum": {
      "0": "Open",
      "1": "Closed",
      "2": "Approved"
    },
    "RoleEnum": {
      "0": "Admin",
      "1": "Staff",
      "2": "Student",
      "3": "Lecturer"
    },
    "ScoresStatusEnum": {
      "0": "IsRetake",
      "1": "IsMock",
      "2": "IsDisable"
    },
    "SubmissionStatusEnum": {
      "0": "Submitted",
      "1": "Unsubmitted",
      "2": "Failed"
    },
    "TeamDetailStatusEnum": {
      "0": "Active",
      "1": "Inactive"
    },
    "UserStatusEnum": {
      "0": "Active",
      "1": "Inactive",
      "2": "Banned"
    }
  }
}
```

## Business rules
- Trả về cấu trúc JSON chứa đầy đủ key-value của các enum được định nghĩa trong `Hackathon.Repository/Enum`.
- Trả về danh sách enum dùng chung nếu FE cần lấy metadata tập trung.
- Với từng API cụ thể có field enum/status, tài liệu của API đó sẽ kèm bảng enum ngay bên dưới response để FE thao tác nhanh.
- Giá trị số trong tài liệu theo mặc định enum C# không gán explicit value: phần tử đầu tiên là `0`, các phần tử sau tăng dần `+1`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "message": "METADATA_LOAD_FAILED",
  "messageCode": "METADATA_LOAD_FAILED",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 500 | METADATA_LOAD_FAILED | METADATA_LOAD_FAILED |
