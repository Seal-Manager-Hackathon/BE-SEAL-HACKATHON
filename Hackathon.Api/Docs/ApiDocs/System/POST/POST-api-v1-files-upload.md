# Upload file hệ thống (File Upload API)

## Tác dụng
Cho phép người dùng upload tài liệu (PDF, Word) hoặc hình ảnh (PNG, JPG) minh chứng lên CDN/Cloud storage của hệ thống để nhận lại link URL lưu vào database (sử dụng cho ảnh đại diện, bài nộp, hoặc bằng chứng khiếu nại).

## URL
`POST /api/v1/files/upload`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- `Authorization: Bearer <AccessToken>`
- `Content-Type: multipart/form-data`

## Request Body (Form Data)
*Định dạng truyền nhận dạng file binary:*
- Key: `file` (Binary file, Bắt buộc)
- Key: `folder` (string, Không bắt buộc, ví dụ: `submissions`, `avatars`, `reports`)

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa link URL lưu trữ.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "FILE_UPLOADED_SUCCESSFULLY",
  "data": {
    "fileUrl": "https://cdn.seal-hackathon.vn/uploads/submissions/file-uuid-name.pdf",
    "fileName": "file-uuid-name.pdf",
    "fileSize": 1048576
  }
}
```

## Business rules
- Định dạng file gửi lên phải khớp với các đuôi mở rộng cho phép (hình ảnh: JPG, PNG; tài liệu: PDF, DOCX, ZIP).
- Dung lượng file tối đa không được vượt quá giới hạn hệ thống (ví dụ: 10MB).
- Tự động gán UUID vào tên file để tránh ghi đè/trùng lặp file trên CDN.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "Bad Request",
  "status": 400,
  "message": "FILE_SIZE_LIMIT_EXCEEDED",
  "messageCode": "FILE_SIZE_LIMIT_EXCEEDED",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | FILE_REQUIRED | FILE_REQUIRED |
| 400 | FILE_SIZE_LIMIT_EXCEEDED | FILE_SIZE_LIMIT_EXCEEDED |
| 400 | INVALID_FILE_TYPE | INVALID_FILE_TYPE |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 500 | FILE_UPLOAD_FAILED | FILE_UPLOAD_FAILED |
