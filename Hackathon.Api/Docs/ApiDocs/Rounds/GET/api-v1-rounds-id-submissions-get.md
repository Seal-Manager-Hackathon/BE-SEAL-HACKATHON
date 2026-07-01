# Student xem bài nộp của team mình trong round

## Tác dụng
Cho phép student (thành viên team) xem bài nộp MỚI NHẤT của team mình trong 1 round.

**Chỉ trả về 1 bài nộp duy nhất — bài mới nhất của team.**  
Các phiên bản cũ bị ẩn — student chỉ thấy được bài cuối cùng.

## URL
`GET /api/v1/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ. Chỉ trả về bài nộp của team mà user là thành viên active.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Có | Id của vòng thi. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `PageIndex` | `int` | Không | Trang số bao nhiêu (Mặc định: `1`). Phải >= 1. |
| `PageSize` | `int` | Không | Số lượng kết quả trên một trang (Mặc định: `10`). Phải >= 1. |

## Ví dụ request
```http
GET /api/v1/rounds/00000000-0000-0000-0000-000000000000/submissions?PageIndex=1&PageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Data": {
    "Items": [
      {
        "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
        "url": "https://github.com/seal-manager/hackathon-project",
        "submittedAt": "2026-06-19T02:15:27Z",
        "status": 0,
        "totalScore": 9.5
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

*Chỉ luôn có 1 item trong mảng — là bài mới nhất. Không có phiên bản cũ.*

## Business rules
- Vòng thi (`roundId`) phải tồn tại và chưa bị vô hiệu hóa (`IsDisable = false`).
- Chỉ trả về các bài nộp thuộc team mà user là thành viên active.
- **Chỉ trả về bài nộp mới nhất** của team (`.FirstOrDefaultAsync()` sau khi sort `SubmittedAt` desc). Nếu team chưa nộp → mảng rỗng.
- Thuộc tính `totalScore` lấy tổng điểm từ bảng điểm (`Scores`) mới nhất. Có thể `null` nếu chưa chấm.

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |
| `1` | Unsubmitted | Chưa nộp bài |
| `2` | Failed | Nộp bài thất bại |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO |
| 400 | BAD_REQUEST | PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
