---
name: api-test
description: Use when the user wants to test API endpoints on the production server, check if APIs work correctly, or verify API behavior before/after deploying code changes
---

# API Test Skill

## Connection Info

- **Server:** `https://api.hkathon.top`
- **Swagger (read-only without auth):** `https://api.hkathon.top/swagger/v1/swagger.json`
- **Swagger UI:** `https://api.hkathon.top/swagger/index.html` (403 without auth)
- **Database:** PostgreSQL at `dpg-d8of2nmrnols73d06k6g-a.singapore-postgres.render.com:5432`, database `seal_vatu`, user `admin`, pass `klSoev6KEkaIjDWapdj1iXfpNz4EVXEP`

## Authentication

```bash
# Login as Admin
EMAIL="admin@hackathon.edu.vn"
PASSWORD="123456Aa@"
curl -s "https://api.hkathon.top/api/v1/auth/login" -H "Content-Type: application/json" \
  -d "{\"email\":\"$EMAIL\",\"password\":\"$PASSWORD\"}"
# → Extract accessToken from response.data.accessToken

# Login as Lecturer
EMAIL="lecturer1@school.edu.vn"
PASSWORD="123456Aa@"
```

## Common Headers

All authenticated requests need:
```bash
-H "Authorization: Bearer <token>"
-H "Content-Type: application/json"
```

## Endpoint Catalog (from Swagger)

### Auth
| Method | Path |
|--------|------|
| GET | /api/v1/auth/me |
| POST | /api/v1/auth/login |
| POST | /api/v1/auth/register |
| POST | /api/v1/auth/logout |
| POST | /api/v1/auth/tokens/refresh |
| POST | /api/v1/auth/email-verifications |
| PATCH | /api/v1/auth/change-password |
| POST | /api/v1/auth/forgot-password |
| POST | /api/v1/auth/reset-password |

### Judge (Lecturer role)
| Method | Path |
|--------|------|
| GET | /api/v1/judge/tracks |
| GET | /api/v1/judge/tracks/{trackId}/submissions |
| GET | /api/v1/judge/events/{eventId}/submissions |
| GET | /api/v1/judge/events/{eventId}/submissions/pending |
| GET | /api/v1/judge/events/{eventId}/submissions/search |
| GET | /api/v1/judge/events/current/submissions/pending |
| GET | /api/v1/judge/submissions/{submissionId}/criteria |
| GET | /api/v1/judge/submissions/{submissionId}/scores/me |
| GET | /api/v1/judge/scores/me |
| GET | /api/v1/judge/submissions/regrade |
| GET | /api/v1/judge/rounds/{roundId}/submissions |
| GET | /api/v1/judge/register-teams/{registerTeamId}/submissions |
| GET | /api/v1/judge/events/{eventId}/rounds/{roundId} |
| GET | /api/v1/judge/events/{eventId}/teams |
| POST | /api/v1/judge/submissions/{submissionId}/scores |
| PATCH | /api/v1/judge/scores/{scoreId} |
| POST | /api/v1/judge/scores/{scoreId}/finalize |
| POST | /api/v1/judge/scores/{scoreId}/retake |

### Student
| Method | Path |
|--------|------|
| POST | /api/v1/teams |
| PATCH | /api/v1/teams/{teamId} |
| GET | /api/v1/teams/{teamId} |
| POST | /api/v1/register-teams |
| GET | /api/v1/rounds/{roundId}/submissions |
| GET | /api/v1/rounds/{roundId}/my-submissions |
| GET | /api/v1/rounds/{roundId}/scores/me |
| GET | /api/v1/rounds/{roundId}/ranking |
| POST | /api/v1/rounds/{roundId}/submit-assignment |
| GET | /api/v1/submissions/{submissionId} |
| POST | /api/v1/events/{eventId}/invitations |

### Admin
| Method | Path |
|--------|------|
| GET | /api/v1/admin/users |
| GET | /api/v1/admin/events/rounds |
| POST | /api/v1/admin/events/rounds |
| PATCH | /api/v1/admin/rounds |
| DELETE | /api/v1/admin/rounds |
| PATCH | /api/v1/admin/rounds/restore |
| POST | /api/v1/admin/events/rounds/criteria |
| GET | /api/v1/admin/events/rounds/criteria |
| PATCH | /api/v1/admin/events/rounds/criteria/activate |
| POST | /api/v1/admin/events |
| PATCH | /api/v1/admin/events |
| DELETE | /api/v1/admin/events |
| GET | /api/v1/admin/events/{eventId}/rounds |
| POST | /api/v1/admin/events/{eventId}/rounds |
| GET | /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria |
| DELETE | /api/v1/admin/rounds/{roundId} |
| PATCH | /api/v1/admin/rounds/{roundId}/restore |
| PATCH | /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria/{templateId}/activate |

### Staff
| Method | Path |
|--------|------|
| GET | /api/v1/staff/rounds/{roundId}/submissions |
| GET | /api/v1/staff/submissions/regrade |
| POST | /api/v1/staff/submissions/{submissionId}/assign-judges |

### Notifications
| Method | Path |
|--------|------|
| GET | /api/v1/notifications/me |
| GET | /api/v1/notifications/me/unread-count |
| PATCH | /api/v1/notifications/{notificationId}/read |
| PATCH | /api/v1/notifications/read-all |
| PATCH | /api/v1/notifications/all/disable |

### Public
| Method | Path |
|--------|------|
| GET | /api/v1/events |
| GET | /api/v1/events/{eventId} |
| GET | /api/v1/events/events/joined |
| GET | /api/v1/events/most-participants |
| GET | /api/v1/rounds/{roundId}/ranking |
| GET | /api/v1/enums |
| GET | /api/v1/tracks |
| GET | /api/v1/roles |

## Test Pattern

Always use this pattern for testing:

```bash
# 1. Login to get token (if not already provided)
TOKEN=$(curl -s "https://api.hkathon.top/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@hackathon.edu.vn","password":"123456Aa@"}' \
  | python3 -c "import sys,json; print(json.load(sys.stdin)['data']['accessToken'])")

# 2. Call the API
curl -s "https://api.hkathon.top/api/v1/..." \
  -H "Authorization: Bearer $TOKEN" \
  | python3 -m json.tool
```

## Response Checking

- HTTP 200 + `"isSuccess": true` → ✅ OK
- HTTP 4xx → ❌ Client error — check params/token
- HTTP 500 → ❌ Server error — check logs/code
- `"isFailed": true` → ❌ Business logic error — check `"message"` field
- Response with 0 items when data exists in DB → check filter logic

## Known Users for Testing

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@hackathon.edu.vn | 123456Aa@ |
| Lecturer | lecturer1@school.edu.vn | 123456Aa@ |
| Student | student1@school.edu.vn | 123456Aa@ |

## Testing After Deploy

After `git push origin develop`, the server at `api.hkathon.top` automatically deploys.
Wait ~30s for the deploy to finish, then run the test sequence.
