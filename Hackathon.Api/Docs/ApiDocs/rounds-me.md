# Xem Vòng thi đang tham gia (Dành cho Student)

### Endpoint & Mô tả
*   **API:** `GET /api/v1/rounds/teams/{teamId}`
*   **Mô tả:** Lấy danh sách các vòng thi (Rounds) mà User hiện tại đang tham gia (thông qua Team cụ thể đã đăng ký và được đưa vào vòng thi đó). Giúp xác định Team đang ở Round nào trong Event.

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Có
*   **Role hợp lệ:** Student (User bình thường)
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team mà user là thành viên.
*   **Query Parameters:** 
    *   `eventId` (Guid, Tùy chọn): Nếu truyền lên, sẽ chỉ trả về vòng thi của User trong sự kiện đó.

### Response Details
*   **Success Response (200 OK):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "SUCCESS",
      "data": [
        {
          "roundId": "8f3b2553-933e-4861-a577-ab6453664d41",
          "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "roundName": "Vòng Sơ loại",
          "eventName": "Hackathon 2026",
          "roundNo": 1,
          "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
          "teamName": "Lập trình viên nghèo",
          "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
          "startTime": "2026-06-20T08:00:00+00:00",
          "endTime": "2026-06-25T17:00:00+00:00",
          "startSubmission": "2026-06-21T08:00:00+00:00",
          "endSubmission": "2026-06-25T17:00:00+00:00"
        }
      ],
      "traceId": "00-84a1e9df64619d8..."
    }
    ```
*   **Error Responses:**
    *   `404 NotFound`: `EVENT_NOT_FOUND` (Nếu có truyền `eventId` nhưng event không tồn tại).
    *   `400 BadRequest`: `INVALID_INPUT` (Nếu ID truyền vào không đúng định dạng Guid).
