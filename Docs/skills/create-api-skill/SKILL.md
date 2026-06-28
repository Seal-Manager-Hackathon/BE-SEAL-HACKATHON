---
name: create-api-skill
description: Use when adding or modifying API endpoints in the Hackathon .NET backend, especially when the user provides an API requirement, request shape, response needs, validation needs, or asks to follow existing Controller-Service patterns
---

# Create API Skill

## Overview

When creating an API in this repository, keep the endpoint aligned with the existing Hackathon backend structure: Controller action -> Service interface -> Service implementation -> Request/Response DTOs -> `ApiResponseFactory.Base(...)` or `ApiResponseFactory.BasePagination(...)`.

The user decides the API purpose, business logic context, and request shape. Treat the user's request shape as the source of truth.

## RESTful API Standards

Endpoints must follow RESTful API design patterns:
1. **Resource Naming**:
   - Use plural nouns for resource paths (e.g., `api/events`, `api/users`, `api/tracks`).
   - Do NOT include verbs in standard CRUD route paths (e.g., avoid `/api/events/get-events`, `/api/events/delete-event`).
2. **HTTP Methods**:
   - `GET`: Retrieve a resource or a paged list. Do not modify server state.
   - `POST`: Create a new resource.
   - `PUT` / `PATCH`: Update an existing resource. Use `PATCH` for partial updates.
   - `DELETE`: Remove a resource.
3. **Custom Actions (Non-CRUD)**:
   - For specific business actions that do not fit basic CRUD (e.g., approve a registration, reply to an invitation), append the action verb at the end of the resource path (e.g., `PATCH api/staff/register-teams/{id:guid}/approve` or `POST api/teams/{id:guid}/invitations`).

## Required Workflow

1. **Identify the target module** from the user's requirement.
   - Prefer an existing controller/service folder when one matches the domain.
   - If no controller exists for the module, **create a new controller using `AuthController.cs` as a template**.
2. **Create Controller actions** by referencing any existing API endpoint pattern inside `AuthController.cs`.
3. **Define Request DTOs** in `Hackathon.Service/<Module>/Request.cs`.
   - Keep the request DTO classes clean and free of validation annotations.
   - Always validate request DTOs using **FluentValidation** by creating a validator class under `Hackathon.Service/Validations/<Module>/` (e.g. `Hackathon.Service/Validations/Auth/ChangePasswordRequestValidator.cs`).
   - For DTOs that represent paginated search queries (e.g., `GetEventsRequest`), **always inherit from `PaginationRequest`** (defined in `Hackathon.Service.Models`).
4. **Define Response DTOs** in `Hackathon.Service/<Module>/Response.cs`.
   - **Always ask the user for response requirements first**, proposing a draft based on the description and related entities before writing code.
   - Do not return EF entities directly.
5. **Add the service method** to `Hackathon.Service/<Module>/IService.cs`.
6. **Implement the business logic** in `Hackathon.Service/<Module>/Service.cs`.
   - Implement the query, sorting, and DTO projection.
   - Do not query the database context directly in controllers.
   - For regular APIs, return the response DTO.
   - For paginated APIs, **return `BasePaginationResponse` directly from the service** using `ApiResponseFactory.BasePagination` and pass `PaginationRequest` as a parameter.
7. **Add the controller action** to invoke the service method.
   - **Paginated endpoints** (service returns `BasePaginationResponse`): return `Ok(result)` directly.

   - **Non-paginated GET/PATCH/DELETE endpoints** (service returns response DTO or `string`): return `Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier))`. Use specific message code instead of `"SUCCESS"` when applicable (e.g. `"TRACK_UPDATED_SUCCESSFULLY"`).

   - **POST create endpoints** (service returns created DTO): return `Created("", ApiResponseFactory.Base(data, 201, "CREATED_MESSAGE", traceId: HttpContext.TraceIdentifier))`, where `data` wraps the returned id if needed: `var data = new { id = result.Id }`.

   - For paginated query parameters in endpoints, **always bind them using `[FromQuery] PaginationRequest paginationRequest`** (or a DTO inheriting from it) instead of individual `pageIndex` / `pageSize` parameters.
8. **Register DI (Dependency Injection)** in `Hackathon.Api/Program.cs` immediately when creating a new service module/class.
   - Add the service registration (e.g., `builder.Services.AddScoped<SomeService.IService, SomeService.Service>();`) in the dependency block inside `Program.cs`.
   - Never skip this step, otherwise the application will crash at runtime with `InvalidOperationException` when resolving the controller dependency.

## Response Envelope Standards

All API responses must strictly conform to the wrapper structures defined in the backend (`ApiResponse` subclasses and `ErrorResponse`). 

1. **PascalCase Response Envelope**:
   The root wrapper properties are serialized using **PascalCase**. Do not use `isSuccess`, `isFailed`, `data`, `error` (camelCase) for envelope roots.
   - Standard: `IsSuccess`, `IsFailed`, `Status`, `Error`, `TraceId`, `TimestampUtc`, `Data`, `Message`.
   - Paginated Standard: `IsSuccess`, `IsFailed`, `Status`, `Error`, `TraceId`, `TimestampUtc`, `Data` (which wraps inner pagination details: `Items`, `PageIndex`, `PageSize`, `TotalCount`, `HasNextPage`, `HasPreviousPage`).

2. **Standard ErrorResponse**:
   Validation failures or system exceptions caught by the global middleware return `ErrorResponse` (without `IsSuccess`, `IsFailed`, or `Data` properties):
   - Fields: `Title`, `Status`, `Message` (not `Detail`), `MessageCode`, `Errors`, `TraceId`, `TimestampUtc`.

3. **Error Specification Table in Docs**:
   Every API markdown documentation must list error codes in a standardized table where:
   - **messageCode**: Must match the exact `MessageCode` defined in the C# custom exception class (e.g. `MISSING_ACCESS_TOKEN`, `UNAUTHORIZED`, `FORBIDDEN`, `EVENT_NOT_FOUND`).
   - **message/detail**: Must match the exact error details string returned by the exception (e.g. `ACCESS_TOKEN_IS_MISSING`, `INVALID_ACCESS_TOKEN`, `Bạn không được phân công hỗ trợ hoặc chấm điểm sự kiện nào.`, `Event không tồn tại.`). Do not write arbitrary local descriptions here.

## Reference Patterns

### Controller Baseline (`AuthController.cs` example)
Use this as the template when creating new controllers. Note the use of `[ApiController]`, route structures, dependency injection of the service interface, and returning wrapping base responses:

```csharp
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthsService.IService _authService;

    public AuthController(AuthsService.IService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthsService.Request.LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        Response.WriteAuthCookies(result.AccessToken!, result.RefreshToken!);
        return Ok(ApiResponseFactory.Base(result, 200, "LOGIN_SUCCESSFUL", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize]
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(AuthsService.Request.ChangePasswordRequest request)
    {
        var message = await _authService.ChangePassword(request);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }
}
```

### Service Implementation Pattern (`Auths/Service.cs` example)
Use this structure when implementing business logic, handling database transactions, and returning response messages:

```csharp
public async Task<string> ChangePassword(Request.ChangePasswordRequest request)
{
    var userId = CheckAccessToken();
    if (userId == Guid.Empty)
    {
        throw new MissingAccessTokenException();
    }

    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
    if (user == null)
    {
        throw new NotFoundException("USER_NOT_FOUND");
    }

    var currentPepperPassword = request.CurrentPassword + _securityOptions.Pepper;
    var isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(
        currentPepperPassword,
        user.HashPassword,
        hashType: BCrypt.Net.HashType.SHA256
    );

    if (!isPasswordValid)
    {
        throw new BadRequestException("CURRENT_PASSWORD_INVALID");
    }

    var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
        var newPepperPassword = request.NewPassword + _securityOptions.Pepper;
        user.HashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newPepperPassword, hashType: BCrypt.Net.HashType.SHA256);
        user.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        await transaction.CommitAsync();
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }

    return "PASSWORD_CHANGED_SUCCESSFULLY";
}
```

## Validation Policy

Always validate request properties using **FluentValidation** under the `Hackathon.Service/Validations/` directory. Create a validator class inheriting from `AbstractValidator<Request.YourRequestDTO>`.

Refer to `Hackathon.Service/Validations/Auth/ChangePasswordRequestValidator.cs` as the gold standard validator pattern:

```csharp
using FluentValidation;
using Hackathon.Service.Auths;

namespace Hackathon.Service.Validations.Auth;

public class ChangePasswordRequestValidator : AbstractValidator<Request.ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("CURRENT_PASSWORD_REQUIRED");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NEW_PASSWORD_REQUIRED")
            .Length(6, 128).WithMessage("NEW_PASSWORD_LENGTH_INVALID")
            .Matches(@"[A-Z]").WithMessage("NEW_PASSWORD_UPPERCASE_REQUIRED")
            .Matches(@"[0-9]").WithMessage("NEW_PASSWORD_DIGIT_REQUIRED")
            .Matches(@"[^a-zA-Z0-9]").WithMessage("NEW_PASSWORD_SPECIAL_CHARACTER_REQUIRED");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("CONFIRM_PASSWORD_REQUIRED")
            .Equal(x => x.NewPassword).WithMessage("CONFIRM_PASSWORD_NOT_MATCH");
    }
}
```

### Common Validation Rules
- Required field: `.NotEmpty().WithMessage("FIELD_REQUIRED")`
- Email format: `.EmailAddress().WithMessage("INVALID_EMAIL_FORMAT")`
- Greater than zero: `.GreaterThan(0).WithMessage("VALUE_MUST_BE_GREATER_THAN_ZERO")`
- Field comparison (e.g. password confirmation): `.Equal(x => x.Password).WithMessage("PASSWORD_CONFIRMATION_NOT_MATCH")`
- Uri validation: `.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("INVALID_URL_FORMAT").When(x => !string.IsNullOrWhiteSpace(x.Url))`

Do not use DataAnnotations validation attributes on the Request DTO classes directly. Keep DTO files clean.

## Transaction Policy

Only use database transactions (`BeginTransactionAsync`) when atomicity is strictly required across multiple table writes:
- **Required**: Complex writes/mutations where a partial failure would cause corrupted state (e.g., creating a team along with its leader member, registering a team to an event).
- **Not Required**: Simple or idempotent actions where duplicate checks prevent issues under network lag or multiple submissions (e.g., sending an invitation which checks for pending invitation first).
- **Rule**: Keep read-only query steps outside of the transaction block. Only start the transaction right before write operations.

## Hard Rules

- Do not use verbs in standard CRUD route paths (e.g., avoid `/delete`, `/update`, `/get` inside resource paths).
- Do not create a new controller if an existing controller clearly owns the API.
- Do not skip `IService.cs` when adding service methods.
- Do not query the database or put business logic directly in controllers.
- Do not return EF entities directly; always map to a response DTO.
- Do not use DataAnnotations validation attributes on Request DTO classes directly.
- Do not perform simple input validation inline in controllers or services; delegate them to FluentValidation.

## Common Mistakes

| Mistake | Correct action |
| --- | --- |
| Using verbs in basic CRUD endpoints (e.g. `[HttpDelete("delete-event/{id}")]`) | Use RESTful noun routes and proper HTTP methods (e.g. `[HttpDelete("{id:guid}")]`) |
| Using DataAnnotations validation attributes on Request DTO classes | Keep Request DTOs clean and write a validator class under `Hackathon.Service/Validations/<Module>/` using FluentValidation (refer to `ChangePasswordRequestValidator.cs` as a template) |
| Querying the database context inside a controller action | Always put queries in the Service implementation |
| Returning EF entities inside the pagination list | Project entities to response DTOs using `.Select()` before `.ToListAsync()` |
| Returning `Ok(ApiResponseFactory.BasePagination(...))` in the controller when the service returns `BasePaginationResponse` | Return `Ok(result)` directly |
