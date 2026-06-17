# Write Frontend API Doc Skill Test Notes

## RED baseline

Without this skill, an agent could fail in the following ways when requested to document completed APIs:

### Scenario 1: Untriggered Documentation Generation
- **User input:** "Hoàn thành api đăng nhập rồi nhé" (without asking to write doc or use the skill).
- **Baseline behavior:** The agent immediately starts writing a full documentation section, which is unnecessary and burns tokens when the user just wanted to notify completion.

### Scenario 2: Documentation Format Discrepancies
- **User input:** "Hoàn thành api CreateTeam, hãy dùng skill viết doc để viết tài liệu cho fe."
- **Baseline behavior:** The agent writes documentation, but:
  - It might miss the custom envelope structure of the project (`ApiResponse` envelope with `success`, `statusCode`, `message`, `data`, `traceId`).
  - It might not detail the exact model state validation errors (like `TEAM_NAME_REQUIRED` for 400 Bad Request).
  - It might not specify the exact roles/permissions and their corresponding error responses (like 403 Forbidden with `USER_PROFILE_NOT_COMPLETED`).

## Skill Requirements derived from baseline
- **Trigger Rule**: Only write the doc when explicitly requested with "sử dụng skill viết doc" or similar and when the API is completed.
- **Envelope Compliance**: All response examples must match the project's `ApiResponse` wrapper.
- **Details Required**: Must include:
  1. API Endpoint (HTTP Method & Path)
  2. Function Description (Miêu tả chức năng)
  3. Permissions/Roles (Phân quyền & Authorization)
  4. Request DTO properties, validations, and custom error messages
  5. Response format (Success and Error scenarios)
