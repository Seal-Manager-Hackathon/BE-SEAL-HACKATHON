# Staff/Admin get round submissions by track & topic

## Tác dụng
Staff/Admin xem danh sách bài nộp của một vòng thi, được phân loại theo track và/hoặc topic kèm thông tin trạng thái chấm điểm chi tiết và judge được phân công. Dùng để theo dõi tiến độ chấm bài và phân công judge chấm cho từng bài nộp.

## URL
`GET /api/v1/staff/rounds/{roundId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Có | Id của vòng thi cần lấy danh sách bài nộp. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `trackId` | `guid` | Không | Lọc theo track/bảng đấu. Có thể dùng đồng thời với `topicId`. |
| `topicId` | `guid` | Không | Lọc theo topic/đề thi. Có thể dùng đồng thời với `trackId`. |
| `submissionStatus` | `string` | Không | Lọc theo trạng thái nộp bài (`Submitted`, `Unsubmitted`, `All`). Mặc định `All`. |
| `gradingStatus` | `string` | Không | Lọc theo trạng thái chấm điểm. Xem bảng bên dưới. Mặc định `All`. |
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên team. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

### Giá trị lọc `gradingStatus`
| Giá trị | Mô tả |
|---|---|
| `All` | Tất cả trạng thái (mặc định) |
| `NoJudgesAssigned` | Đã nộp bài nhưng chưa được phân công judge |
| `PendingGrading` | Đã phân công judge nhưng chưa ai chấm |
| `GradingInProgress` | Đang chấm dở (một số judge đã chấm, một số chưa) |
| `Graded` | Tất cả judge đã chấm xong nhưng chưa chốt điểm |
| `Finalized` | Tất cả judge đã chốt điểm (không sửa được nữa) |

## Ví dụ request
```http
GET /api/v1/staff/rounds/00000000-0000-0000-0000-000000000000/submissions?trackId=00000000-0000-0000-0000-000000000000&topicId=00000000-0000-0000-0000-000000000000&gradingStatus=GradingInProgress&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount)`.

### Cấu trúc item (một bài nộp)
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
  "teamName": "Chiến binh công nghệ",
  "trackId": "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e",
  "trackTitle": "Web Development",
  "topicId": "5c6d7e8f-9a0b-1c2d-3e4f-5a6b7c8d9e0f",
  "topicTitle": "Xây dựng nền tảng học trực tuyến",
  "url": "https://github.com/seal-manager/hackathon-project",
  "description": "Bài thi hoàn chỉnh.",
  "submissionStatus": "Submitted",
  "submittedAt": "2026-06-19T02:15:27Z",
  "gradingStatus": "GradingInProgress",
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "email": "nguyenvana@school.edu.vn",
      "hasScored": true,
      "totalScore": 85.0,
      "isFinalized": false
    },
    {
      "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
      "judgeName": "Trần Thị B",
      "email": "tranthib@school.edu.vn",
      "hasScored": false,
      "totalScore": null,
      "isFinalized": false
    }
  ],
  "averageScore": 85.0,
  "minScore": 85.0,
  "maxScore": 85.0
}
```

---

### Trường hợp 1: Team chưa nộp bài (Unsubmitted)
Team đã được duyệt vào event, đã vào round này nhưng chưa nộp bài. Mặc dù chưa có bài nộp, danh sách judge của track tương ứng vẫn được trả về (từ `AssignTracks`).
```json
{
  "submissionId": null,
  "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
  "teamName": "Team chưa nộp",
  "trackId": "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e",
  "trackTitle": "Web Development",
  "topicId": "5c6d7e8f-9a0b-1c2d-3e4f-5a6b7c8d9e0f",
  "topicTitle": "Xây dựng nền tảng học trực tuyến",
  "url": null,
  "description": null,
  "submissionStatus": "Unsubmitted",
  "submittedAt": null,
  "gradingStatus": null,
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "email": "nguyenvana@school.edu.vn",
      "hasScored": false,
      "totalScore": null,
      "isFinalized": false
    }
  ],
  "averageScore": null,
  "minScore": null,
  "maxScore": null
}
```

---

### Trường hợp 2: Đã nộp bài, chưa phân công judge (NoJudgesAssigned)
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
  "teamName": "Team ABC",
  "url": "https://github.com/seal-manager/hackathon-project",
  "submissionStatus": "Submitted",
  "submittedAt": "2026-06-19T02:15:27Z",
  "gradingStatus": "NoJudgesAssigned",
  "assignedJudges": [],
  "averageScore": null,
  "minScore": null,
  "maxScore": null
}
```

---

### Trường hợp 3: Đã nộp, đã phân công judge nhưng chưa ai chấm (PendingGrading)
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "teamName": "Team ABC",
  "submissionStatus": "Submitted",
  "gradingStatus": "PendingGrading",
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "hasScored": false,
      "totalScore": null,
      "isFinalized": false
    },
    {
      "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
      "judgeName": "Trần Thị B",
      "hasScored": false,
      "totalScore": null,
      "isFinalized": false
    }
  ],
  "averageScore": null,
  "minScore": null,
  "maxScore": null
}
```

---

### Trường hợp 4: Đang chấm dở - một số judge đã chấm, một số chưa (GradingInProgress)
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "teamName": "Team ABC",
  "submissionStatus": "Submitted",
  "gradingStatus": "GradingInProgress",
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "hasScored": true,
      "totalScore": 85.0,
      "isFinalized": false
    },
    {
      "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
      "judgeName": "Trần Thị B",
      "hasScored": false,
      "totalScore": null,
      "isFinalized": false
    }
  ],
  "averageScore": 85.0,
  "minScore": 85.0,
  "maxScore": 85.0
}
```

---

### Trường hợp 5: Tất cả judge đã chấm xong, có điểm đầy đủ nhưng chưa chốt (Graded)
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "teamName": "Team ABC",
  "submissionStatus": "Submitted",
  "gradingStatus": "Graded",
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "hasScored": true,
      "totalScore": 85.0,
      "isFinalized": false
    },
    {
      "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
      "judgeName": "Trần Thị B",
      "hasScored": true,
      "totalScore": 90.0,
      "isFinalized": false
    }
  ],
  "averageScore": 87.5,
  "minScore": 85.0,
  "maxScore": 90.0
}
```

---

### Trường hợp 6: Tất cả judge đã chốt điểm (Finalized)
> **Lưu ý:** Trạng thái `Finalized` và `isFinalized = true` hiện chưa thể đạt được do domain score-finalization chưa được implement. Trường hợp này được mô tả để FE chuẩn bị UI trước.
```json
{
  "submissionId": "8fa95f64-5717-4562-b3fc-2c963f66afa6",
  "teamName": "Team ABC",
  "submissionStatus": "Submitted",
  "gradingStatus": "Finalized",
  "assignedJudges": [
    {
      "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
      "judgeName": "Nguyễn Văn A",
      "hasScored": true,
      "totalScore": 85.0,
      "isFinalized": true
    },
    {
      "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
      "judgeName": "Trần Thị B",
      "hasScored": true,
      "totalScore": 90.0,
      "isFinalized": true
    }
  ],
  "averageScore": 87.5,
  "minScore": 85.0,
  "maxScore": 90.0
}
```

---

### Response tổng thể
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": null,
  "timestampUtc": "datetime",
  "data": {
    "items": [
      { /* item theo 1 trong 6 trường hợp trên */ }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 50,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

### Filter theo track và/hoặc topic
API cho phép kết hợp đồng thời cả `trackId` và `topicId` trong query. Các trường hợp lọc:

| trackId | topicId | Kết quả |
|---|---|---|
| Có | Không | Lấy submission của team thuộc track đó (bất kỳ topic nào) |
| Không | Có | Lấy submission của team được gán topic đó |
| Có | Có | Lấy submission của team thuộc track đó VÀ được gán đúng topic đó |
| Không | Không | Lấy tất cả submission của round |

## Business rules
- **Staff/Admin thấy được TẤT CẢ các phiên bản nộp bài của mỗi team** (không filter lấy bài mới nhất). Mỗi lần team nộp lại là 1 item riêng.
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này yêu cầu role `Staff` hoặc `Admin` qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `roundId` là bắt buộc trên path.
- Round phải tồn tại và chưa bị soft-disable, nếu không trả `ROUND_NOT_FOUND`.
- Nếu người gọi là Staff: phải được phân công vào event chứa round đó (`AssignEvents`) thì mới được xem, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công.
- `trackId` và `topicId` có thể dùng đồng thời để lọc chéo (intersection filter).
- Nếu team đã được duyệt vào event và có `RoundDetails` cho round này nhưng chưa có submission, trả về `submissionStatus = "Unsubmitted"` để Staff biết đội nào chưa nộp.
- Đối với team chưa nộp bài, danh sách `assignedJudges` vẫn được trả về dựa trên `AssignTracks` của track (nếu track có judge).
- `averageScore` được tính là trung bình cộng tổng điểm của các judge đã chấm (`totalScore` có giá trị).
- `minScore` và `maxScore` là điểm thấp nhất và cao nhất trong các judge đã chấm.
- `isFinalized` ở cấp judge cho biết judge đó đã chốt điểm chưa (không cho sửa nữa).
- Khi tất cả judge đã `isFinalized = true` thì `gradingStatus` chuyển thành `"Finalized"`.
- Bài nộp bị disable (`IsDisable = true`) được loại khỏi danh sách kết quả.

### Bảng tổng hợp trạng thái
| submissionStatus | gradingStatus | Ý nghĩa |
|---|---|---|
| `Unsubmitted` | `null` | Team chưa nộp bài |
| `Submitted` | `NoJudgesAssigned` | Đã nộp, chưa có judge nào được phân công |
| `Submitted` | `PendingGrading` | Có judge nhưng chưa ai chấm |
| `Submitted` | `GradingInProgress` | Đang chấm dở (một số judge đã chấm) |
| `Submitted` | `Graded` | Tất cả judge đã chấm xong, đủ điểm |
| `Submitted` | `Finalized` | Tất cả judge đã chốt điểm |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
