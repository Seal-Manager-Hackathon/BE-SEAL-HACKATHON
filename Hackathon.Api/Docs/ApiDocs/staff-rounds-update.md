# Chỉnh sửa Round (Dành cho Admin và Staff)

### Endpoint & Mô tả
*   **API:** `PUT /api/v1/rounds/{roundId}` (hoặc `PATCH /api/v1/rounds/{roundId}`)
*   **Mô tả:** Cập nhật thông tin chi tiết của 1 vòng thi (thời gian, giới hạn đội...).

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Có
*   **Role hợp lệ:** Staff / Admin
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)
    *   `403 Forbidden` (`STAFF_NOT_ASSIGNED_TO_EVENT` dựa vào EventId của Round đang sửa).

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Route Parameters:**
    *   `roundId`: ID vòng thi cần chỉnh sửa.
*   **Body JSON:** (Có thể gửi một phần hoặc toàn bộ trường)
    ```json
    {
      "name": "Vòng Sơ loại (Cập nhật)",
      "description": "Mô tả mới...",
      "roundNo": 2,
      "startTime": "2026-06-20T08:00:00Z",
      "endTime": "2026-06-26T17:00:00Z",
      "startSubmission": "2026-06-21T08:00:00Z",
      "endSubmission": "2026-06-26T17:00:00Z",
      "limitTeam": 100
    }
    ```
*   **Ràng buộc validation:**
    *   `name`: Nếu có thì không được trống (`ROUND_NAME_REQUIRED`).
    *   `roundNo`: Nếu có thì phải lớn hơn 0 (`INVALID_ROUND_NO`).
    *   Thời gian `startTime`/`endTime` và `startSubmission`/`endSubmission` tuân thủ nguyên tắc kết thúc phải sau bắt đầu (`INVALID_ROUND_TIME`, `INVALID_SUBMISSION_TIME`).
    *   `limitTeam` > 0 (`INVALID_LIMIT_TEAM`).

### Response Details
*   **Success Response (200 OK):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "ROUND_UPDATED_SUCCESSFULLY",
      "data": {
        "id": "8f3b2553-933e-4861-a577-ab6453664d41",
        "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "Vòng Sơ loại (Cập nhật)",
        "description": "Mô tả mới...",
        "roundNo": 2,
        "startTime": "2026-06-20T08:00:00+00:00",
        "endTime": "2026-06-26T17:00:00+00:00",
        "startSubmission": "2026-06-21T08:00:00+00:00",
        "endSubmission": "2026-06-26T17:00:00+00:00",
        "limitTeam": 100,
        "isDisable": false,
        "createdAt": "2026-06-18T10:00:00+00:00"
      },
      "traceId": "00-84a1e9df64619d8..."
    }
    ```
*   **Error Responses:**
    *   `404 NotFound`: `ROUND_NOT_FOUND` (Round không tồn tại hoặc đã bị xóa).
    *   `403 Forbidden`: `STAFF_NOT_ASSIGNED_TO_EVENT`.
    *   `409 Conflict`: `ROUND_NO_ALREADY_EXISTS` (Nếu đổi sang một RoundNo đã tồn tại).
