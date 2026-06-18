# Implementation Plan: API to Get Team Event Registrations

**Goal:** Create an API (`GET /api/v1/teams/{teamId}/events`) for users to see which events a specific team has requested to join, including status, time, and registration ID. It must support filtering by status and pagination.

## Global Constraints
- Use the existing `ApiResponseFactory.BasePagination` for response format.
- Ensure correct database queries (Entity Framework Core) using `AsNoTracking` for read-only operations.

### Task 1: Create Response Model
- **File:** `Hackathon.Service/RegisterTeams/Response.cs` (or `Hackathon.Service/Teams/Response.cs` depending on the current structure; verify first).
- **Action:** Define a class `RegisteredEventItemResponse` containing:
  - `RegisterId` (Guid)
  - `TeamId` (Guid)
  - `TeamName` (string)
  - `EventId` (Guid)
  - `EventName` (string)
  - `Status` (string)
  - `Description` (string?)
  - `CreatedAt` (DateTimeOffset)

### Task 2: Create Request Model
- **File:** `Hackathon.Service/RegisterTeams/Request.cs` (or `Hackathon.Service/Teams/Request.cs`).
- **Action:** Define a class `GetTeamRegisteredEventsRequest` containing:
  - `Status` (string?)

### Task 3: Implement Service Logic
- **File:** `Hackathon.Service/Teams/IService.cs` and `Hackathon.Service/Teams/Service.cs` (or `RegisterTeams` equivalent, verify current home for team-centric event lookups).
- **Action:** Add method `GetTeamRegisteredEvents(Guid teamId, GetTeamRegisteredEventsRequest request, PaginationRequest paginationRequest)`
- **Logic:**
  1. Validate `teamId` exists.
  2. Query `RegisterTeams` where `TeamId == teamId`.
  3. Include `Event` and `Team` entities.
  4. Filter by `request.Status` (if provided, parse `RegisterTeamStatusEnum`).
  5. Apply sorting (Pending -> Approved -> Rejected, or by CreatedAt descending if status is filtered).
  6. Apply pagination (`Skip` and `Take`).
  7. Return `BasePaginationResponse` mapping data to `RegisteredEventItemResponse`.

### Task 4: Add Controller Endpoint
- **File:** `Hackathon.Api/Controllers/TeamController.cs`.
- **Action:** Add `[HttpGet("{teamId:guid}/events")]` mapping to the new service method.

### Task 5: Create API Documentation
- **File:** `Hackathon.Api/Docs/ApiDocs/api-v1-teams-id-events-get.md`.
- **Action:** Document the new endpoint matching the required Markdown structure.
