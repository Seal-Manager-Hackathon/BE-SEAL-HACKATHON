# Xem danh sách đội đã được duyệt trong event

## Tác dụng
API dành cho Staff, Lecturer hoặc Admin (đã được phân công trong event) xem danh sách các đội thi đã được duyệt (`Approved`) tham gia một event, kèm thông tin track/topic đã gán, trạng thái thi đấu (đã bị loại hay chưa), vòng thi hiện tại, và hỗ trợ tìm kiếm theo tên đội.

## URL
`GET /api/v1/register-teams/events/{eventId}/approved-teams`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`, `Lecturer` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách đội đã được duyệt. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên đội (không phân biệt hoa thường). |
| `isEliminated` | `bool` | Không | Lọc theo trạng thái thi đấu. `true`: chỉ lấy đội đã bị loại, `false`: chỉ lấy đội đang thi đấu. Nếu không truyền thì lấy cả hai. |

## Ví dụ request
```http
GET /api/v1/register-teams/events/00000000-0000-0000-0000-000000000000/approved-teams?keyword=abc&isEliminated=false
Authorization: Bearer {accessToken}
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
      "currentRoundId": "guid|null",
      "currentRoundName": "string|null",
      "currentRoundNo": 1,
      "isEliminated": false
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
- Yêu cầu access token hợp lệ.
- Endpoint này dùng policy `StaffLecturerOrAdminPolicy` (Staff, Lecturer, Admin).
- `eventId` là bắt buộc trên path.
- Event phải tồn tại và chưa bị disable, nếu không trả `EVENT_NOT_FOUND`.
- Nếu người gọi là Staff hoặc Lecturer: phải được phân công vào event đó (`AssignEvents`) thì mới được xem, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công.
- API **chỉ trả về** các đơn đăng ký có trạng thái `Approved` (đã được duyệt) và chưa bị soft-disable.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo tên đội (`Team.Name`).
- Nếu truyền `isEliminated`, lọc theo trạng thái thi đấu (tính toán động).
- **Cách tính `IsEliminated` (động):**
  - Nếu event chưa có round nào active → tất cả đội đều `isEliminated = false` (chưa bắt đầu).
  - Nếu event có round active: đội bị loại khi **không có** `RoundDetails` active trong bất kỳ round active nào.
- **Cách tính `CurrentRound` (động):**
  - Vòng thi hiện tại = round active có `RoundNo` lớn nhất mà đội có `RoundDetails`.
  - Nếu đội đã bị loại hoặc event chưa có round → `currentRoundId` / `currentRoundName` / `currentRoundNo` = `null`.
- **Sắp xếp:**
  1. Đội chưa bị loại (`isEliminated = false`) lên trước, đội đã bị loại xuống sau.
  2. Trong cùng nhóm, sắp xếp theo tên đội (`Team.Name`) tăng dần, sau đó `CreatedAt` giảm dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | EVENT_ID_REQUIRED |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeamController`.
- Method: `GetApprovedTeams(Guid eventId, Request.GetApprovedTeamsRequest request)`.
- Endpoint dùng route `GET /api/v1/register-teams/events/{eventId:guid}/approved-teams` và `StaffLecturerOrAdminPolicy`.
