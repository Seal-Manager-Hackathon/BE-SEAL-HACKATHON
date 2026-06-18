# Lấy danh sách Round của 1 Event (Dành cho Admin và Staff)

### Endpoint & Mô tả
*   **API:** `GET /api/v1/rounds?eventId={eventId}` hoặc `GET /api/v1/staff/events/{eventId}/rounds`
*   **Mô tả:** Xem toàn bộ các vòng thi (Round) cấu hình bên trong một Event.

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Có
*   **Role hợp lệ:** Staff / Admin
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)
    *   `403 Forbidden` (`STAFF_NOT_ASSIGNED_TO_EVENT` nếu Staff không nằm trong AssignEvents của Event).

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Query Parameters:** 
    *   `eventId` (Guid, Bắt buộc nếu gọi từ `/api/v1/rounds`): ID của sự kiện.
    *   `isDisable` (Boolean, Tùy chọn): Lọc theo trạng thái xóa mềm.

### Response Details
*   **Success Response (200 OK):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "SUCCESS",
      "data": [
        {
          "id": "8f3b2553-933e-4861-a577-ab6453664d41",
          "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "name": "Vòng Sơ loại",
          "description": "Mô tả chi tiết vòng sơ loại...",
          "startTime": "2026-06-20T08:00:00+00:00",
          "endTime": "2026-06-25T17:00:00+00:00",
          "startSubmission": "2026-06-21T08:00:00+00:00",
          "endSubmission": "2026-06-25T17:00:00+00:00",
          "limitTeam": 50,
          "isDisable": false,
          "createdAt": "2026-06-18T10:00:00+00:00"
        }
      ],
      "traceId": "00-84a1e9df64619d8..."
    }
    ```
*   **Error Responses:**
    *   `400 BadRequest`: `EVENT_ID_REQUIRED`.
    *   `404 NotFound`: `EVENT_NOT_FOUND`.
    *   `403 Forbidden`: `STAFF_NOT_ASSIGNED_TO_EVENT`.
