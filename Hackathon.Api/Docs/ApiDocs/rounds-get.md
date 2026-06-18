# Lấy danh sách Round của 1 Event (Dành cho Student / Public)

### Endpoint & Mô tả
*   **API:** `GET /api/v1/rounds?eventId={eventId}` 
*   **Mô tả:** Lấy danh sách các vòng thi (Round) của một Event đang diễn ra. API này trả về danh sách các Round đang hoạt động (không bị disable).

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Tùy thuộc vào thiết kế (Thường là không bắt buộc hoặc yêu cầu Student).
*   **Role hợp lệ:** Public hoặc Student.
*   **Lỗi phân quyền:**
    *   (Nếu yêu cầu Auth) `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token> (Nếu có)
    Content-Type: application/json
    ```
*   **Query Parameters:** 
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện muốn xem vòng thi.

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
    *   `404 NotFound`: `EVENT_NOT_FOUND` (Nếu Event bị disable hoặc không tồn tại).
