# Xóa mềm (Disable) / Khôi phục Round (Dành cho Admin và Staff)

### Endpoint & Mô tả
*   **API:** `PUT /api/v1/rounds/{roundId}/disable` (hoặc `DELETE /api/v1/rounds/{roundId}`)
*   **Mô tả:** Ẩn (xóa mềm) vòng thi.

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** Có
*   **Role hợp lệ:** Staff / Admin
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)
    *   `403 Forbidden` (`STAFF_NOT_ASSIGNED_TO_EVENT`).

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Route Parameters:**
    *   `roundId`: ID vòng thi.
*   **Body JSON:** (Tùy chọn)
    ```json
    {
       "isDisable": true
    }
    ```

### Response Details
*   **Success Response (200 OK):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "ROUND_DISABLED_SUCCESSFULLY",
      "data": null,
      "traceId": "00-84a1e9df64619d8..."
    }
    ```
*   **Error Responses:**
    *   `404 NotFound`: `ROUND_NOT_FOUND`.
    *   `403 Forbidden`: `STAFF_NOT_ASSIGNED_TO_EVENT`.
