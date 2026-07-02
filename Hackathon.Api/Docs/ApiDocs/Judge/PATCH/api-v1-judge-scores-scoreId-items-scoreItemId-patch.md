# Judge sửa điểm từng tiêu chí (Judge Update Score Item)

## Tác dụng
Giúp Judge sửa điểm hoặc comment của 1 tiêu chí (ScoreItem) riêng lẻ, không cần gửi lại tất cả.

## URL
`PATCH /api/v1/judge/scores/{scoreId}/items/{scoreItemId}`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công chấm bài này.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `scoreId` | `guid` | Có | ID của bảng điểm. |
| `scoreItemId` | `guid` | Có | ID của tiêu chí cần sửa. |

## Request body
```json
{
  "score": 45,
  "comment": "Cập nhật nhận xét"
}
```
Cả 2 field đều optional — chỉ gửi field nào cần sửa.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "message": "SCORE_ITEM_UPDATED_SUCCESSFULLY",
  "data": {
    "criteriaItemId": "guid",
    "criteriaItemName": "Tính thực tiễn",
    "score": 45.0,
    "comment": "Cập nhật nhận xét"
  }
}
```

## Business rules
- Score phải thuộc về judge đang request.
- ScoreItem phải thuộc Score đó.
- ScoreItem phải chưa bị disable.
- Score không được âm và không vượt quá `maxScore` của criteria item.
- Hệ thống tự động tính lại `TotalScore` của Score sau khi sửa.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | SCORE_MUST_BE_NON_NEGATIVE / SCORE_LIMIT_EXCEEDED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | SCORE_ITEM_NOT_OWNED_BY_JUDGE |
| 404 | NOT_FOUND | SCORE_ITEM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
