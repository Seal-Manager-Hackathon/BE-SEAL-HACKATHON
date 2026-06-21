# Admin update event

## Tác dụng
Admin cập nhật một phần thông tin của một event đã tồn tại.

## URL
`PATCH /api/v1/admin/events/{eventId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần cập nhật. |

## Request body
Chỉ cần truyền các field muốn cập nhật.

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
  "status": 1, /* Published */
  "numberRound": 0,
  "season": "string|null"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `name` | `string` | Không | Tên mới của event. Nếu truyền thì không được để trống. |
| `description` | `string` | Không | Mô tả của event. |
| `startTime` | `datetime` | Không | Thời gian bắt đầu event. |
| `endTime` | `datetime` | Không | Thời gian kết thúc event. |
| `registerLimitTime` | `datetime` | Không | Thời gian hạn chót đăng ký. |
| `limitTeam` | `int` | Không | Số lượng team tối đa có thể đăng ký. |
| `minMember` | `int` | Không | Số lượng thành viên tối thiểu mỗi team. |
| `maxMember` | `int` | Không | Số lượng thành viên tối đa mỗi team. |
| `status` | `enum` | Không | Trạng thái của event. Giá trị: `Draft`, `Published`, `Closed`, `Cancelled`. Nếu không truyền sẽ giữ nguyên trạng thái hiện tại. | // 0: Draft, 1: Published, 2: Closed, 3: Cancelled
| `numberRound` | `int` | Không | Số vòng thi của event. |
| `season` | `string` | Không | Mùa/mùa giải của event. |

## Response body
Response dùng `ApiResponseFactory.Base(data)` và chỉ trả message khi cập nhật thành công.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": "EVENT_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này bắt buộc Admin-only qua `[Authorize(Policy = JwtExtensions.AdminPolicy)]`.
- `eventId` là bắt buộc trên path.
- Event phải tồn tại, nếu không trả `EVENT_NOT_FOUND`.
- API là partial update: field nào không truyền thì giữ nguyên giá trị hiện tại.
- `name` không bắt buộc. Nếu truyền thì không được để trống hoặc chỉ chứa khoảng trắng, nếu không trả `EVENT_NAME_REQUIRED`.
- Nếu truyền `name`, tên mới không được trùng với event khác (không phân biệt hoa thường), nếu không trả `EVENT_NAME_ALREADY_EXISTS`.
- `status` nếu truyền được bind trực tiếp vào `EventStatusEnum` (`Draft`, `Published`, `Closed`, `Cancelled`). Nếu không truyền hoặc truyền `null`, sẽ giữ nguyên trạng thái hiện tại của event. // 0: Draft, 1: Published, 2: Closed, 3: Cancelled
- `UpdatedAt` được cập nhật theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- `startTime` phải diễn ra trước `endTime` (so với giá trị mới nếu truyền, hoặc so với giá trị cũ nếu giữ nguyên), nếu không trả lỗi `START_TIME_MUST_BE_BEFORE_END_TIME`.
- `registerLimitTime` phải diễn ra trước `startTime` (so với giá trị mới nếu truyền, hoặc so với giá trị cũ nếu giữ nguyên), nếu không trả lỗi `REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME`.
- Khi cập nhật thành công chỉ trả message `EVENT_UPDATED_SUCCESSFULLY`, không trả data event.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 400 | BAD_REQUEST | EVENT_NAME_REQUIRED |
| 400 | BAD_REQUEST | INVALID_EVENT_STATUS |
| 400 | BAD_REQUEST | START_TIME_MUST_BE_BEFORE_END_TIME |
| 400 | BAD_REQUEST | REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | EVENT_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `UpdateEvent(Guid eventId, UpdateEventRequest request)` trong `Hackathon.Service.Events.IService`.
- Đã thêm request model `UpdateEventRequest` trong `Hackathon.Service.Events.Request`.
- Đã implement logic partial update trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/events/{eventId}` và `AdminPolicy`.
- Response thành công chỉ trả message `EVENT_UPDATED_SUCCESSFULLY`.


## Ghi chú Enum
Tham chiếu file [00-enum-values.md](00-enum-values.md) để biết chi tiết các giá trị số (int) trả về cho các trường Trạng thái (Status).
