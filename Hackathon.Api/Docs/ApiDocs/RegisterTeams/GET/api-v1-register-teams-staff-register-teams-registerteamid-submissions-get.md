# Xem bài nộp và điểm của đội (theo round hoặc tất cả)

## Tác dụng
Staff hoặc Admin xem tất cả bài nộp của một đội trong event. Có thể lọc theo round cụ thể hoặc lấy toàn bộ. Trả về danh sách bài nộp kèm trạng thái chấm điểm, điểm số chi tiết theo tiêu chí, roundId, roundNo, và đánh dấu bài nộp mới nhất (`isLatest`).

## URL
`GET /api/v1/register-teams/staff/register-teams/{registerTeamId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký (đội) cần xem bài nộp. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Không | Lọc theo vòng. Không truyền = lấy tất cả bài nộp của đội trong mọi round. |

## Ví dụ request
```http
# Lấy tất cả bài nộp của đội
GET /api/v1/register-teams/staff/register-teams/22222222-2222-2222-2222-222222222222/submissions
Authorization: Bearer {accessToken}

# Lọc theo round cụ thể
GET /api/v1/register-teams/staff/register-teams/22222222-2222-2222-2222-222222222222/submissions?roundId=11111111-1111-1111-1111-111111111111
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(data)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "registerTeamId": "guid",
    "teamId": "guid",
    "teamName": "string",
    "trackId": "guid|null",
    "trackTitle": "string|null",
    "submissions": [
      {
        "submissionId": "guid",
        "roundId": "guid",
        "roundNo": 1,
        "url": "string|null",
        "description": "string|null",
        "status": 0,
        "submittedAt": "datetime|null",
        "isLatest": true,
        "gradingStatus": "Graded",
        "score": {
          "scoreId": "guid",
          "totalScore": 85.5,
          "isRetake": false,
          "isMock": false,
          "scoreItems": [
            {
              "scoreItemId": "guid",
              "criteriaItemId": "guid",
              "criteriaItemName": "Code Quality",
              "score": 18.0,
              "maxScore": 20.0,
              "comment": "Good structure"
            }
          ]
        }
      }
    ]
  },
  "message": "SUCCESS"
}
```

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thành công |
| `1` | Unsubmitted | Chưa nộp bài / Đang soạn |
| `2` | Failed | Nộp bài thất bại |

### Bảng ý nghĩa các trường
| Trường | Ý nghĩa |
|--------|---------|
| `roundId` / `roundNo` | Vòng mà bài nộp này thuộc về |
| `isLatest` | `true` nếu là bài nộp mới nhất trong round đó |
| `gradingStatus` | `"Graded"` nếu bài đã chấm, `"NotGraded"` nếu chưa |
| `score.totalScore` | Tổng điểm |
| `score.scoreItems` | Điểm chi tiết từng tiêu chí |

## Business rules
- Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.
- Endpoint dùng policy `StaffOrAdminPolicy`.
- `registerTeamId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Nếu truyền `roundId`: round phải tồn tại, thuộc cùng event với register team, và chưa bị disable, nếu không trả `ROUND_NOT_FOUND`.
- Nếu không truyền `roundId`: lấy tất cả bài nộp của đội trong tất cả round của event.
- Phải có ít nhất một bản ghi `RoundDetails` khớp (register team + round nếu lọc), nếu không trả `ROUND_DETAIL_NOT_FOUND`.
- `isLatest` được tính riêng cho từng round (mỗi round có một bài mới nhất).
- Sắp xếp submissions theo `SubmittedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeamController`.
- Method: `GetTeamRoundSubmissions(Guid registerTeamId, Guid? roundId)`.
- Endpoint dùng route `GET /api/v1/register-teams/staff/register-teams/{registerTeamId:guid}/submissions` và `StaffOrAdminPolicy`.
