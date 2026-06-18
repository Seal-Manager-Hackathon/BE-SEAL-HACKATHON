---
name: write-fe-api-doc-skill
description: Use when the user explicitly requests frontend API documentation for a completed API endpoint, using instructions like "hoàn thành api ..." and directing the use of the doc writing skill
---

# Write FE API Doc Skill

## Overview
Generates standardized API documentation for the frontend team matching the Hackathon project structures, authentication mechanisms, validation models, and response envelopes.

## Trigger Rule
*   **Only generate** the documentation when the user states that an API is completed (e.g. "hoàn thành api...") **AND** explicitly asks to write documentation or use this skill (e.g. "dùng skill viết doc").
*   If the user only notifies completion without requesting the documentation/skill, just acknowledge and ask if they need it documented.

## Core Template
Produce the documentation in Vietnamese following this exact markdown template:

### Endpoint & Mô tả
*   **API:** `[METHOD] /api/v1/...` (e.g. `POST /api/v1/teams`)
*   **Mô tả:** [Chức năng của API]

### Phân quyền (Permissions)
*   **Yêu cầu Access Token:** [Có/Không]
*   **Role hợp lệ:** [Student / Staff / Admin / Public]
*   **Lỗi phân quyền:**
    *   `401 Unauthorized` (`MISSING_ACCESS_TOKEN` / `INVALID_ACCESS_TOKEN`)
    *   `403 Forbidden` (Nếu role không khớp)

### Request Details
*   **Headers:**
    ```
    Authorization: Bearer <token>
    Content-Type: application/json
    ```
*   **Route / Query Parameters:** (nếu có)
*   **Body JSON:** (nếu có)
    ```json
    { ... }
    ```
*   **Ràng buộc validation:**
    *   `[Trường]`: [Loại validation, e.g. Bắt buộc, Range] -> Trả về lỗi `400 Bad Request` với message code tương ứng (e.g. `TEAM_NAME_REQUIRED`).

### Response Details
*   **Success Response (200 OK / 201 Created):**
    ```json
    {
      "success": true,
      "statusCode": 200,
      "message": "...",
      "data": { ... },
      "traceId": "..."
    }
    ```
*   **Error Responses:**
    Liệt kê chi tiết mã lỗi (e.g. `400 BadRequest`, `404 NotFound`, `409 Conflict`) kèm theo `messageCode` tương ứng (e.g. `TEAM_NAME_ALREADY_EXISTS`, `USER_PROFILE_NOT_COMPLETED`).

## Common Mistakes
*   **Lỗi:** Trả về data trực tiếp không bọc qua `ApiResponse` envelope.
    *   *Khắc phục:* Luôn bọc data/message/status trong đúng format success/error envelope của dự án.
*   **Lỗi:** Tự động viết tài liệu khi người dùng chỉ nhắn "hoàn thành api..." mà không yêu cầu viết tài liệu.
    *   *Khắc phục:* Acknowledge trước, đợi yêu cầu cụ thể mới dùng skill này viết tài liệu.
