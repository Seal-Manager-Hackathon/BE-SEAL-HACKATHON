# Staff/Admin kết thúc vòng đấu (End Round)

## Tác dụng
Kết thúc vòng đấu hiện tại và chuyển các team có điểm cao nhất (dựa trên giới hạn số team của vòng tiếp theo) sang vòng tiếp theo.

## URL
`POST /api/v1/rounds/{roundId}/end`

## Authorization
Yêu cầu access token hợp lệ với policy `StaffOrAdminPolicy` (role `Staff` hoặc `Admin`).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id của round (vòng đấu) hiện tại cần kết thúc. |

## Query parameters
Không có.

## Ví dụ request
```http
POST /api/v1/rounds/9cb15a44-1234-4562-a3fc-3d963f66bfb9/end
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": {
    "closedRoundId": "9cb15a44-1234-4562-a3fc-3d963f66bfb9",
    "nextRoundId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalTeamsAdvanced": 5
  },
  "message": "ROUND_ENDED_SUCCESSFULLY | FINAL_ROUND_CLOSED_HACKATHON_ENDED"
}
```

## Business rules
- Người gọi API phải có role `Staff` hoặc `Admin`.
- Round được gọi phải tồn tại và chưa bị disable (`IsDisable = false`).
- Các team từ round hiện tại sẽ được đánh giá qua điểm `TotalScore` cao nhất từ các submission không bị disable.
- Số lượng team đi tiếp được quyết định bởi trường `LimitTeam` của round tiếp theo (round có cùng `EventId` và `RoundNo = current RoundNo + 1`).
- Nếu không có vòng tiếp theo, thông báo trả về sẽ là `FINAL_ROUND_CLOSED_HACKATHON_ENDED` và `nextRoundId` sẽ là `null`.
- Round hiện tại sau đó sẽ bị đóng (được set `IsDisable = true`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | User does not have the required permissions. |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
