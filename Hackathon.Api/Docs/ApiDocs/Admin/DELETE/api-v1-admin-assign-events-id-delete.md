# DELETE /api/v1/admin/assign-events/{id}

**Role:** Admin
**Policy:** AdminPolicy

## Mô tả

Xoá (soft-delete) một staff assignment khỏi event. Set IsDisable = true cho record trong bảng AssignEvents.

## Request

### Route Parameters

| Param | Type | Mô tả |
|-------|------|-------|
| id | Guid | ID của assign event |

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | STAFF_ASSIGNMENT_REMOVED_SUCCESSFULLY |
| 404 | ASSIGN_EVENT_NOT_FOUND |

### Body

```json
{
  "data": null,
  "message": "STAFF_ASSIGNMENT_REMOVED_SUCCESSFULLY"
}
```
