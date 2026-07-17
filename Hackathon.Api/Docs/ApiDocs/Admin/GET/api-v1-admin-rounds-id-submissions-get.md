# Admin lấy danh sách bài nộp của round (Admin Get Round Submissions)

## Tác dụng
Admin lấy danh sách tất cả bài nộp (Submissions) thuộc một vòng thi (Round), hỗ trợ lọc theo trạng thái và phân trang.

## URL
`GET /api/v1/admin/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi cần lấy danh sách bài nộp. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `status` | `string` | Không | Lọc theo trạng thái bài nộp (`Submitted`, `Graded`, `Failed`). |
| `pageIndex` | `int` | Không | Trang hiện tại. Mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang. Mặc định `10`, tối đa `100`. |

## Ví dụ request
```http
GET /api/v1/admin/rounds/3fa85f64-5717-4562-b3fc-2c963f66afa6/submissions?status=Graded&pageIndex=1&pageSize=20
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "data": {
    "items": [
      {
        "id": "guid",
        "roundDetailId": "guid",
        "status": "Graded",
        "isRegrade": false,
        "imgUrl": null,
        "fileUrl": null,
        "description": "string",
        "teamName": "string",
        "trackName": "string",
        "totalScore": 85.0,
        "createdAt": "2026-07-01T10:00:00+00:00"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 1
  },
  "message": "SUCCESS"
}
```

## Business rules
- Người gọi phải có role `Admin`.
- `roundId` phải là GUID hợp lệ.
- Round phải tồn tại. Nếu không, trả `404 Not Found` (`ROUND_NOT_FOUND`).
- Nếu không truyền `status`, trả tất cả bài nộp của round.
- Kết quả sắp xếp theo `CreatedAt` giảm dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
