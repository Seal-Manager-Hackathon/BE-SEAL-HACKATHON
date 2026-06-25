# Judge xác nhận gửi điểm (Judge Confirm Score)

## Tác dụng
Giúp Judge xác nhận bảng điểm của mình đã sẵn sàng gửi cho BTC. API này kiểm tra quyền sở hữu và tính hợp lệ của bảng điểm, không khóa trạng thái để chặn PATCH sau đó.

## URL
`POST /api/v1/judge/scores/{scoreId}/finalize`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên sở hữu bảng điểm.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `scoreId` | `guid` | Có | ID của bảng điểm. |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SCORE_FINALIZED",
  "data": "SCORE_FINALIZED"
}
```

## Business rules
- Bảng điểm phải tồn tại trong DB, người gọi phải là chủ nhân bảng điểm.
- Bảng điểm phải có ít nhất một `ScoreItem` đang hoạt động.
- Không cho xác nhận điểm mock (`IsMock = true`).
- Không ghi trạng thái khóa điểm theo schema hiện tại.
- Sau khi xác nhận, Judge vẫn có thể PATCH điểm theo schema hiện tại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "Bạn không có quyền quản lý bảng điểm này.",
  "messageCode": "SCORE_NOT_OWNED_BY_JUDGE",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | MOCK_SCORE_CANNOT_BE_FINALIZED | Không thể xác nhận điểm mock. |
| 400 | SCORE_ITEMS_REQUIRED | Bảng điểm chưa có điểm chi tiết. |
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | SCORE_NOT_OWNED_BY_JUDGE | Bảng điểm không thuộc về người gọi. |
| 404 | SCORE_NOT_FOUND | Bảng điểm không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `POST /api/v1/judge/scores/{scoreId}/finalize`.
- Sử dụng policy `LecturerPolicy`.
