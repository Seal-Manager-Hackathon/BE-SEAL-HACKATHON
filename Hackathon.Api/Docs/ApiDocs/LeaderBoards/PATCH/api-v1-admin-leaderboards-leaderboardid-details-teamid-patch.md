# BTC gán giải thưởng trên Leaderboard

## Tác dụng
Cho phép BTC gán giải thưởng đạt được (`LevelAward`) và điều chỉnh điểm số thủ công cho một team cụ thể trên leaderboard.

## URL
`PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `leaderBoardId` | `guid` | Có | ID của Leaderboard. |
| `teamId` | `guid` | Có | ID của team cần gán giải. |

## Query parameters
Không có.

## Ví dụ request
```http
PATCH /api/v1/admin/leaderboards/00000000-0000-0000-0000-000000000000/details/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
Content-Type: application/json
```

## Request body
```json
{
  "score": 0.0,
  "levelAward": 1
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,  
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": "AWARD_ASSIGNED_SUCCESSFULLY",
  "message": "AWARD_ASSIGNED_SUCCESSFULLY"
}
```

## Business rules
- Bản ghi `LeaderBoardDetails` liên kết `leaderBoardId` và `teamId` phải tồn tại.
- Cập nhật trường `Score` và `LevelAward` tương ứng trong bảng `LeaderBoardDetails` (BR-LB-06).
- Chỉ cho phép chỉnh sửa khi sự kiện và leaderboard chưa bị khóa (read-only).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | LEADERBOARD_DETAIL_NOT_FOUND | Không tìm thấy thông tin xếp hạng của đội. |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
