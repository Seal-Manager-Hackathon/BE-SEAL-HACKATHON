# Lấy danh sách bài nộp của Vòng thi (Get Round Submissions)

## Tác dụng
Lấy danh sách các bài nộp (submissions) của một vòng thi cụ thể (Round), hỗ trợ phân trang.

## URL
`GET /api/v1/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `roundId` | `guid` | Có | Id của vòng thi. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `PageIndex` | `int` | Không | Trang số bao nhiêu (Mặc định: `1`). Phải >= 1. |
| `PageSize` | `int` | Không | Số lượng kết quả trên một trang (Mặc định: `10`). Phải >= 1. |

## Ví dụ request
```http
GET /api/v1/rounds/00000000-0000-0000-0000-000000000000/submissions?PageIndex=1&PageSize=10
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
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
        "url": "https://github.com/seal-manager/hackathon-project",
        "submittedAt": "2026-06-19T02:15:27Z",
        "status": "0",
        "totalScore": 9.5
      },
      {
        "submissionId": "2cb15a44-1234-4562-a3fc-3d963f66bfb9",
        "url": "https://drive.google.com/file/d/123456",
        "submittedAt": "2026-06-19T01:00:10Z",
        "status": "0",
        "totalScore": null
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 2,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Vòng thi (`roundId`) phải tồn tại và chưa bị vô hiệu hóa (`IsDisable = false`).
- Chỉ trả về các bài nộp thuộc vòng thi được chỉ định và chưa bị vô hiệu hóa.
- Danh sách trả về được sắp xếp theo thời gian nộp bài (`SubmittedAt`) giảm dần (mới nhất lên đầu).
- Thuộc tính `totalScore` sẽ lấy tổng điểm từ bảng điểm (`Scores`) mới nhất liên quan đến bài nộp. Có thể `null` nếu bài nộp chưa được chấm điểm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO, PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |