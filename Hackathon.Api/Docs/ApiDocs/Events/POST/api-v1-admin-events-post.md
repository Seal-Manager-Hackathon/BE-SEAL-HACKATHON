# Admin create event

## Tác dụng
Admin tạo một event mới trong hệ thống.

## URL
`POST /api/v1/admin/events`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Request body
```json
{
  "name": "string",
  "description": "string|null",
  "startTime": "datetime|null",
  "endTime": "datetime|null",
  "registerLimitTime": "datetime|null",
  "limitTeam": 0,
  "minMember": 0,
  "maxMember": 0,
  "numberRound": 0,
  "season": "Summer"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `name` | `string` | Có | Tên của event. |
| `description` | `string` | Không | Mô tả của event. |
| `startTime` | `datetime` | Không | Thời gian bắt đầu event. |
| `endTime` | `datetime` | Không | Thời gian kết thúc event. |
| `registerLimitTime` | `datetime` | Không | Thời gian hạn chót đăng ký. |
| `limitTeam` | `int` | Không | Số lượng team tối đa có thể đăng ký. |
| `minMember` | `int` | Không | Số lượng thành viên tối thiểu mỗi team. |
| `maxMember` | `int` | Không | Số lượng thành viên tối đa mỗi team. |
| `numberRound` | `int` | Không | Số vòng thi của event. |
| `season` | `SeasonEnum` | Không | Mùa giải: `Spring`, `Summer`, `Autumn`, `Winter`. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 201,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "guid"
  },
  "message": "EVENT_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này bắt buộc Admin-only qua `[Authorize(Policy = JwtExtensions.AdminPolicy)]`.
- `name` là bắt buộc, không được để trống hoặc chỉ chứa khoảng trắng, nếu không trả `EVENT_NAME_REQUIRED`.
- Tên event không được trùng với event khác (không phân biệt hoa thường), nếu không trả `EVENT_NAME_ALREADY_EXISTS`.
- Khi tạo mới, event mặc định có `Status = Draft`, `IsDisable = false`.
- `CreatedAt` và `UpdatedAt` được set theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- Các field không bắt buộc nếu không truyền sẽ để `null`.
- `startTime` phải diễn ra trước `endTime` (nếu cả 2 có giá trị), nếu không trả về lỗi `START_TIME_MUST_BE_BEFORE_END_TIME`.
- `registerLimitTime` phải diễn ra trước `startTime` (nếu cả 2 có giá trị), nếu không trả về lỗi `REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME`.
- `startTime`, `endTime`, `registerLimitTime` nếu có thì phải theo đúng format ISO 8601.
- Khi tạo thành công chỉ trả `id` của event vừa tạo và message `EVENT_CREATED_SUCCESSFULLY`, không trả data event.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 400 | BAD_REQUEST | EVENT_NAME_REQUIRED |
| 400 | BAD_REQUEST | START_TIME_MUST_BE_BEFORE_END_TIME |
| 400 | BAD_REQUEST | REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME |
| 409 | CONFLICT | EVENT_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `CreateEvent(CreateEventRequest request)` trong `Hackathon.Service.Events.IService`.
- Đã thêm request model `CreateEventRequest` trong `Hackathon.Service.Events.Request`.
- Đã thêm response model `CreateEventResponse` trong `Hackathon.Service.Events.Response`.
- Endpoint dùng route `POST /api/v1/admin/events` và `AdminPolicy`.
- Response thành công chỉ trả `id` và message `EVENT_CREATED_SUCCESSFULLY`.
