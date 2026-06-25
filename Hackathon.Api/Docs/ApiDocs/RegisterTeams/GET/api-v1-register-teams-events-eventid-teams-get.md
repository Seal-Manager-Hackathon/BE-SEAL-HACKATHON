# Xem danh sách đội thi trong event (có thể lọc theo round và/hoặc track)

## Tác dụng
API dành cho người dùng đã đăng nhập (bất kỳ role nào) xem danh sách các đội thi trong một event. Hỗ trợ lọc theo round (`roundId`) và/hoặc track (`trackId`). Nếu không truyền round, lấy tất cả đội trong event. Nếu không truyền track, lấy tất cả track.

## URL
`GET /api/v1/register-teams/events/{eventId}/teams`

## Authorization
Yêu cầu access token hợp lệ (bất kỳ role nào: Student, Staff, Lecturer, Admin).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách đội thi. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Không | Lọc theo vòng. Chỉ lấy các đội có trong round này. Không truyền = lấy tất cả round. |
| `trackId` | `guid` | Không | Lọc theo track. Chỉ lấy các đội thuộc track này. Không truyền = lấy tất cả track. |

## Ví dụ request
```http
# Lấy tất cả đội trong event
GET /api/v1/register-teams/events/00000000-0000-0000-0000-000000000000/teams
Authorization: Bearer {accessToken}

# Lọc theo round
GET /api/v1/register-teams/events/00000000-0000-0000-0000-000000000000/teams?roundId=11111111-1111-1111-1111-111111111111

# Lọc theo track
GET /api/v1/register-teams/events/00000000-0000-0000-0000-000000000000/teams?trackId=22222222-2222-2222-2222-222222222222

# Lọc theo cả round và track
GET /api/v1/register-teams/events/00000000-0000-0000-0000-000000000000/teams?roundId=11111111-1111-1111-1111-111111111111&trackId=22222222-2222-2222-2222-222222222222
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(data)` — trả về mảng `data` là danh sách đội.

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
      "registerTeamId": "guid",
      "teamId": "guid",
      "teamName": "string",
      "trackId": "guid|null",
      "trackTitle": "string|null",
      "topicId": "guid|null",
      "topicTitle": "string|null",
      "status": 1,
      "isBanned": false,
      "createdAt": "datetime"
    }
  ],
  "message": "SUCCESS"
}
```

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt |
| `1` | Approved | Đã được duyệt |
| `2` | Rejected | Bị từ chối |

## Business rules
- Yêu cầu access token hợp lệ (bất kỳ role nào).
- `eventId` là bắt buộc trên path.
- Event phải tồn tại và chưa bị disable, nếu không trả `EVENT_NOT_FOUND`.
- Nếu truyền `roundId`: round phải tồn tại, thuộc event và chưa bị disable, nếu không trả `ROUND_NOT_FOUND`.
- Nếu truyền `trackId`: track phải tồn tại, thuộc event và chưa bị disable, nếu không trả `TRACK_NOT_FOUND`.
- Nếu không truyền `roundId`: trả về tất cả đội đã đăng ký trong event (không lọc theo round).
- Nếu không truyền `trackId`: trả về tất cả đội (không lọc theo track).
- Nếu truyền cả `roundId` và `trackId`: trả về đội vừa trong round đó vừa thuộc track đó.
- Kết quả chỉ bao gồm các đơn đăng ký chưa bị soft-disable và team chưa bị soft-disable.
- Sắp xếp theo tên đội (`TeamName`) tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | EVENT_ID_REQUIRED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeamController`.
- Method: `GetTeamsByRound(Guid eventId, Request.GetTeamsByRoundRequest request)`.
- Endpoint dùng route `GET /api/v1/register-teams/events/{eventId:guid}/teams` và `[Authorize]` (bất kỳ role nào).
