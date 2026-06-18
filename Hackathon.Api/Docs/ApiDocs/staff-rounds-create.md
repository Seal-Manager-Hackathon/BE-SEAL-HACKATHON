# Tạo mới Round (Dành cho Admin và Staff)

### Endpoint & Mô tả
*   **API:** `POST /api/v1/rounds` (hoặc `POST /api/v1/staff/events/{eventId}/rounds`)
*   **Mô tả:** Admin hoặc Staff được phân công sẽ tạo một vòng thi mới cho một sự kiện (Event).

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Có
*   **Role hợp lệ:** Staff / Admin
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)
    *   `403 Forbidden` (`STAFF_NOT_ASSIGNED_TO_EVENT` nếu Staff không nằm trong AssignEvents của Event đó).

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Body JSON:**
    ```json
    {
      "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "Vòng Sơ loại",
      "description": "Mô tả chi tiết vòng sơ loại...",
      "startTime": "2026-06-20T08:00:00Z",
      "endTime": "2026-06-25T17:00:00Z",
      "startSubmission": "2026-06-21T08:00:00Z",
      "endSubmission": "2026-06-25T17:00:00Z",
      "limitTeam": 50
    }
    ```
*   **Ràng buộc validation:**
    *   `eventId`: Bắt buộc. -> Trả về lỗi `400 Bad Request` với message code `EVENT_ID_REQUIRED`.
    *   `name`: Bắt buộc. -> Trả về lỗi `400 Bad Request` với message code `ROUND_NAME_REQUIRED`.
    *   `startTime` & `endTime`: Nếu có truyền vào thì `endTime` phải lớn hơn `startTime`. -> Trả về `400 Bad Request` với message code `INVALID_ROUND_TIME`.
    *   `startSubmission` & `endSubmission`: Nếu có truyền vào thì `endSubmission` phải lớn hơn `startSubmission`. -> Trả về `400 Bad Request` với message code `INVALID_SUBMISSION_TIME`.
    *   `limitTeam`: Nếu có thì phải > 0. -> Trả về `400 Bad Request` với message code `INVALID_LIMIT_TEAM`.

### Response Details
*   **Success Response (200 OK):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "ROUND_CREATED_SUCCESSFULLY",
      "data": {
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
      },
      "traceId": "00-84a1e9df64619d8..."
    }
    ```
*   **Error Responses:**
    *   `404 NotFound`: `EVENT_NOT_FOUND` (EventId không tồn tại hoặc đã bị disable).
    *   `403 Forbidden`: `STAFF_NOT_ASSIGNED_TO_EVENT` (Staff không phụ trách sự kiện này).
