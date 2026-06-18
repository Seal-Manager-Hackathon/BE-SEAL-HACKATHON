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
Produce the documentation in Vietnamese. Use `Hackathon.Api/Docs/ApiDocs/staff-register-teams-by-event-get.md` as the absolute gold standard for structure, layout, envelope wrapping (`isSuccess`, `value`, `error`), and markdown formatting. 

DO NOT output your own markdown headers like "Endpoint & Mô tả". Instead, use the exact headers found in the reference file:

- `# [Action Name]`
- `## Tác dụng`
- `## URL`
- `## Authorization`
- `## Path parameters` (nếu có)
- `## Query parameters` (nếu có)
- `## Ví dụ request`
- `## Request body`
- `## Response body`
- `## Business rules`
- `## Lỗi có thể xảy ra` (Dùng định dạng bảng y hệt file reference)

### Specific Instructions:
- **Response Format:** Follow the `ApiResponseFactory.Base` or `ApiResponseFactory.BasePagination` envelope model carefully:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "error": null,
    "traceId": "string",
    "timestampUtc": "datetime",
    "value": { ... }
  }
  ```
- **Error Responses:**
    Dựa vào logic của Service để viết chi tiết các mã HTTP Status Code (e.g. `400 BadRequest`, `404 NotFound`, `409 Conflict`) kèm theo các giá trị `messageCode` tương ứng sẽ xuất hiện (e.g. `TEAM_NAME_ALREADY_EXISTS`, `USER_PROFILE_NOT_COMPLETED`). Trình bày dưới dạng Markdown Table.
- **Business rules:**
    Trình bày dưới dạng list gạch đầu dòng (`- `) giống hệt file reference. Đảm bảo liệt kê đầy đủ các validation quan trọng.

## Common Mistakes
*   **Lỗi:** Trả về data trực tiếp không bọc qua `ApiResponse` envelope.
    *   *Khắc phục:* Luôn bọc data/message/status trong đúng format success/error envelope của dự án.
*   **Lỗi:** Tự động viết tài liệu khi người dùng chỉ nhắn "hoàn thành api..." mà không yêu cầu viết tài liệu.
    *   *Khắc phục:* Acknowledge trước, đợi yêu cầu cụ thể mới dùng skill này viết tài liệu.
