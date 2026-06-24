# Xem bảng xếp hạng chung cuộc Event (Get Event Leaderboard)

## Tác dụng
Xem bảng xếp hạng chung cuộc của một event thi đấu (điểm chung cuộc = tổng điểm các round thi đấu của team). FE dùng danh sách này để hiển thị mỗi team đang đứng hạng thứ mấy trong event đó.

## URL
`GET /api/v1/events/{eventId}/leaderboard`

## Authorization
Public (không yêu cầu access token). Chỉ hiển thị khi leaderboard đã được BTC công bố.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": [
    {
      "rank": 1,
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "totalScore": 270.5,
      "levelAward": "Giải Nhất"
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Event phải tồn tại trong DB và không bị soft-disable.
- Chỉ hiển thị khi leaderboard đã được BTC công bố công khai (published).
- Xếp hạng được sắp xếp theo tổng điểm (`totalScore`) giảm dần.
- Trường `rank` cho biết team đang đứng hạng thứ mấy trong event.
- `levelAward` hiển thị danh hiệu đạt được (Nhất, Nhì, Ba, Khuyến khích) nếu BTC đã gán.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 403 | FORBIDDEN | LEADERBOARD_NOT_PUBLISHED_YET |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `LeaderBoards` + `LeaderBoardDetails`.
