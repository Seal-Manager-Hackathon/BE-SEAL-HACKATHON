# Admin lấy danh sách vòng thi của event (Admin Get Rounds)

## Tác dụng
Admin lấy danh sách tất cả vòng thi (Rounds) thuộc một sự kiện (Event), bao gồm cả round đang active và round đã bị soft-disable. Hỗ trợ lọc theo trạng thái disable và phân trang.

## URL
`GET /api/v1/admin/events/{eventId}/rounds`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.
Policy: `AdminPolicy`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện cần lấy danh sách round. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-delete. `true`: chỉ round đã disable; `false`: chỉ round active. Không truyền: trả tất cả. |
| `pageIndex` | `int` | Không | Trang hiện tại. Mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang. Mặc định `10`, tối đa `100`. |

## Ví dụ request
```http
GET /api/v1/admin/events/3fa85f64-5717-4562-b3fc-2c963f66afa6/rounds?isDisable=false&pageIndex=1&pageSize=20
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "id": "guid",
        "eventId": "guid",
        "name": "Vòng Sơ loại",
        "description": "Vòng thi đầu tiên của cuộc thi",
        "roundNo": 1,
        "startTime": "2026-07-01T09:00:00+00:00",
        "endTime": "2026-07-03T18:00:00+00:00",
        "startSubmission": "2026-07-01T09:00:00+00:00",
        "endSubmission": "2026-07-03T12:00:00+00:00",
        "limitTeam": 20,
        "isDisable": false,
        "createdAt": "2026-06-30T10:00:00+00:00"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  },
  "message": "SUCCESS"
}
```

## Business rules
- Người gọi phải có role `Admin`.
- `eventId` phải là GUID hợp lệ trên path.
- Event phải tồn tại. Nếu không, trả `404 Not Found` (`EVENT_NOT_FOUND`).
- Nếu không truyền `isDisable`, trả về tất cả round của event (cả active và disabled).
- Nếu `isDisable = false`: chỉ trả round active (`IsDisable == false`).
- Nếu `isDisable = true`: chỉ trả round đã soft-delete (`IsDisable == true`).
- Nếu event tồn tại nhưng không có round phù hợp filter, trả `items: []`, `totalCount: 0`.
- `pageIndex` nhỏ hơn `1` nên được normalize về `1` hoặc trả lỗi validate theo chuẩn pagination hiện có.
- `pageSize` nhỏ hơn `1` nên dùng mặc định; lớn hơn `100` nên cap về `100` hoặc trả lỗi validate theo chuẩn pagination hiện có.
- Kết quả sắp xếp theo `RoundNo` tăng dần, sau đó `CreatedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | INVALID_PAGINATION |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.AdminController`.
- Route hiện có: `GET /api/v1/admin/events/{eventId}/rounds`.
- Sử dụng policy `AdminPolicy` (attribute trên controller class).
- Service: `Hackathon.Service.Admin.Service.GetRounds()`.
- DTO: `GetAdminRoundsRequest` (`AdminService.Request`) gồm `isDisable`, `pageIndex`, `pageSize`.
- Response item: `AdminRoundResponse` (`AdminService.Response`) chứa tất cả field của Round.
- Entity: `Rounds` — query `AsNoTracking`, filter theo `EventId`, optional `IsDisable`.
- Kiểm tra tồn tại event trước, không check IsDisable của event.
- Phân trang: normalize PageIndex về 1, PageSize clamp trong [1..100].
- Sắp xếp: `RoundNo` tăng dần, sau đó `CreatedAt` tăng dần.
