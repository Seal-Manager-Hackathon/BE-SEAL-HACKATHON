# Nhật ký Cập nhật API và Codebase (update.md)

---

## 1. Mở rộng API GetRegisterTeamDetail (Staff/Admin → Staff/Lecturer/Admin + thêm IsEliminated & CurrentRound)

### Thay đổi trong Code:

#### 1.1 Policy Authorization mở rộng cho Lecturer
| Mục | Trước thay đổi | Sau thay đổi |
|-----|----------------|--------------|
| **Controller policy** | `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]` | `[Authorize(Policy = JwtExtensions.StaffLecturerOrAdminPolicy)]` |
| **Đường dẫn** | [RegisterTeamController.cs](Hackathon.Api/Controllers/RegisterTeamController.cs)#58 | [RegisterTeamController.cs](Hackathon.Api/Controllers/RegisterTeamController.cs)#58 |

#### 1.2 Response DTO bổ sung trường
| Trường | Kiểu | Mô tả |
|--------|------|-------|
| `IsEliminated` | `bool` | true = đội đã bị loại, false = đội đang thi đấu |
| `CurrentRoundId` | `Guid?` | Id của vòng thi hiện tại (nếu còn thi đấu) |
| `CurrentRoundName` | `string?` | Tên vòng thi hiện tại |
| `CurrentRoundNo` | `int?` | Số thứ tự vòng thi hiện tại |

- **File:** [RegisterTeams/Response.cs](Hackathon.Service/RegisterTeams/Response.cs)#25-44

#### 1.3 Logic tính IsEliminated & CurrentRound động trong Service
- **Trước thay đổi:** Không có — response không có các trường này.
- **Sau thay đổi** tại [RegisterTeams/Service.cs](Hackathon.Service/RegisterTeams/Service.cs)#464-540:
  - Query tất cả active rounds của event.
  - Nếu event chưa có round → `IsEliminated = false`, `CurrentRound* = null`.
  - Nếu có round → kiểm tra `RoundDetails`:
    - Không có `RoundDetails` active → `IsEliminated = true`.
    - Có `RoundDetails` active → `IsEliminated = false`, `CurrentRound*` = round active có `RoundNo` cao nhất.

### Thay đổi trong Response JSON (camelCase)

**Trước đây — chỉ có:**
```json
{
  "data": {
    "id": "guid", "teamId": "guid", "teamName": "string",
    "trackId": "guid|null", "trackTitle": "string|null",
    "topicId": "guid|null", "topicTitle": "string|null",
    "status": 0, "isBanned": false, "isDisable": false,
    "members": [],
    "createdAt": "datetime", "updatedAt": "datetime"
  }
}
```

**Sau thay đổi — thêm các trường mới:**
```json
{
  "data": {
    "id": "guid", "teamId": "guid", "teamName": "string",
    "trackId": "guid|null", "trackTitle": "string|null",
    "topicId": "guid|null", "topicTitle": "string|null",
    "status": 0, "isBanned": false, "isDisable": false,
    "isEliminated": false,
    "currentRoundId": "guid|null",
    "currentRoundName": "string|null",
    "currentRoundNo": 1,
    "members": [],
    "createdAt": "datetime", "updatedAt": "datetime"
  }
}
```

---

## 2. Tạo mới API GetTeamsByTrack & GetApprovedTeams

Cả 2 API mới được tạo với:
- Request DTO riêng (gom `keyword`, `status`, `isEliminated`)
- Kiểu trả về tuple `(List<T> Data, string Message)` — khi không có team → `NO_TEAMS_FOUND`
- Tính `IsEliminated` + `CurrentRound*` động
- Sort: chưa bị loại trước, Team.Name asc, CreatedAt desc

### 2.1 Endpoint mới

| Endpoint | Route | Method |
|----------|-------|--------|
| GetTeamsByTrack | `GET /api/v1/register-teams/events/{eventId}/tracks/{trackId}/teams` | [RegisterTeamController.cs](Hackathon.Api/Controllers/RegisterTeamController.cs)#98-107 |
| GetApprovedTeams | `GET /api/v1/register-teams/events/{eventId}/approved-teams` | [RegisterTeamController.cs](Hackathon.Api/Controllers/RegisterTeamController.cs)#109-117 |

### 2.2 Authorization

Cả 2 đều dùng `[Authorize(Policy = JwtExtensions.StaffLecturerOrAdminPolicy)]` (Staff, Lecturer, Admin).

### 2.3 Request DTOs mới

**Trước — tham số riêng lẻ:**
```csharp
Task<List<RegisterTeamTrackResponse>> GetTeamsByTrack(Guid eventId, Guid trackId, string? keyword, RegisterTeamStatusEnum? status, bool? isEliminated);
Task<List<RegisterTeamApprovedResponse>> GetApprovedTeams(Guid eventId, string? keyword, bool? isEliminated);
```

**Sau — gom vào Request DTO:**
```csharp
Task<(List<RegisterTeamTrackResponse> Data, string Message)> GetTeamsByTrack(Guid eventId, Guid trackId, Request.GetTeamsByTrackRequest request);
Task<(List<RegisterTeamApprovedResponse> Data, string Message)> GetApprovedTeams(Guid eventId, Request.GetApprovedTeamsRequest request);
```

### 2.4 Request classes trong [Request.cs](Hackathon.Service/RegisterTeams/Request.cs)

```csharp
public class GetTeamsByTrackRequest
{
    public string? Keyword { get; set; }
    public RegisterTeamStatusEnum? Status { get; set; }
    public bool? IsEliminated { get; set; }
}

public class GetApprovedTeamsRequest
{
    public string? Keyword { get; set; }
    public bool? IsEliminated { get; set; }
}
```

### 2.5 Response DTOs trong [Response.cs](Hackathon.Service/RegisterTeams/Response.cs)

| DTO | Trường | Kiểu |
|-----|--------|------|
| `RegisterTeamTrackResponse` | `RegisterTeamId`, `TeamId`, `TeamName`, `Status`, `TopicId`, `TopicTitle`, `CurrentRoundId`, `CurrentRoundName`, `CurrentRoundNo`, `IsEliminated` | `List<T>` |
| `RegisterTeamApprovedResponse` | giống + thêm `TrackId`, `TrackTitle` | `List<T>` |

### 2.6 Message trả về

| Trường hợp | Message |
|------------|---------|
| Có team trong kết quả | `"SUCCESS"` |
| **Không có team nào** | **`"NO_TEAMS_FOUND"`** |

### 2.7 Response JSON mẫu

**GetTeamsByTrack — thành công có dữ liệu:**
```json
{
  "isSuccess": true, "isFailed": false, "status": 200,
  "data": [
    {
      "registerTeamId": "guid", "teamId": "guid", "teamName": "string",
      "status": 1,
      "topicId": "guid|null", "topicTitle": "string|null",
      "currentRoundId": "guid|null", "currentRoundName": "string|null", "currentRoundNo": 1,
      "isEliminated": false
    }
  ],
  "message": "SUCCESS"
}
```

**GetApprovedTeams — không có dữ liệu:**
```json
{
  "isSuccess": true, "isFailed": false, "status": 200,
  "data": [],
  "message": "NO_TEAMS_FOUND"
}
```

### 2.8 Tài liệu API

- [api-v1-register-teams-events-eventid-tracks-trackid-teams-get.md](Hackathon.Api/Docs/ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-tracks-trackid-teams-get.md)
- [api-v1-register-teams-events-eventid-approved-teams-get.md](Hackathon.Api/Docs/ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-approved-teams-get.md)

---

## 3. Thay đổi khác

### 3.1 Policy mới trong [JwtExtensions.cs](Hackathon.Api/Extention/JwtExtensions.cs)

Thêm policy `StaffLecturerOrAdminPolicy` — cho phép Staff, Lecturer, Admin.

```csharp
public const string StaffLecturerOrAdminPolicy = "StaffLecturerOrAdminPolicy";

options.AddPolicy(StaffLecturerOrAdminPolicy, policy =>
    policy.RequireRole(RoleEnum.Staff.ToString(),
                       RoleEnum.Lecturer.ToString(),
                       RoleEnum.Admin.ToString()));
```

### 3.2 Fix corrupt tail trong [Service.cs](Hackathon.Service/RegisterTeams/Service.cs)

`UnbanRegisterTeam` bị thiếu closing braces (file bị hỏng bởi planning text), đã phục hồi đúng cú pháp.
