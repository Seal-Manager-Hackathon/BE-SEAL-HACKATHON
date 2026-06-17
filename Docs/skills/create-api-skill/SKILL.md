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
   - Always validate properties on the DTO model class using standard **DataAnnotations** attributes (e.g., `[Required]`, `[EmailAddress]`, `[Range]`, `[Compare]`).
   - Do not use `IValidatableObject` or FluentValidation for request validation when DataAnnotations can express the rule.
4. **Define Response DTOs** in `Hackathon.Service/<Module>/Response.cs`.
   - **Always ask the user for response requirements first**, proposing a draft based on the description and related entities before writing code.
   - Do not return EF entities directly.
5. **Add the service method** to `Hackathon.Service/<Module>/IService.cs`.
6. **Implement the business logic** in `Hackathon.Service/<Module>/Service.cs`.
   - Implement the query, sorting, and DTO projection.
   - Do not query the database context directly in controllers.
   - For regular APIs, return the response DTO.
   - For paginated APIs, **return `BasePaginationResponse` directly from the service** using `ApiResponseFactory.BasePagination` (modeled after the reference screenshot).
7. **Add the controller action** to invoke the service method.
   - Regular endpoints return `Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier))`.
   - Paginated endpoints return `Ok(result)` directly since the service already bakes the `BasePaginationResponse` structure.
8. **Register DI (Dependency Injection)** in `Hackathon.Api/Program.cs` immediately when creating a new service module/class.
   - Add the service registration (e.g., `builder.Services.AddScoped<SomeService.IService, SomeService.Service>();`) in the dependency block inside `Program.cs`.
   - Never skip this step, otherwise the application will crash at runtime with `InvalidOperationException` when resolving the controller dependency.

## Reference Patterns

### Controller Baseline (`AuthController.cs` example)
Use this as the template when creating new controllers. Note the use of `[ApiController]`, route structures, dependency injection of the service interface, and returning wrapping base responses:

```csharp
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService.IService _authService;

    public AuthController(AuthService.IService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthService.Request.LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
```

### Paginated API Pattern (Reference Screenshot Example)
Use this structure when implementing pagination.

**Service implementation (`Service.cs`):**
```csharp
public async Task<BasePaginationResponse> GetNewsAsync(int pageIndex, int pageSize, string? keyword, NewsStatus? status)
{
    pageIndex = pageIndex <= 0 ? 1 : pageIndex;
    pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);

    var query = _dbContext.NewsList
        .AsNoTracking()
        .Include(news => news.CategoryNewsDetail)
        .Where(news => !news.IsDeleted);

    if (!string.IsNullOrWhiteSpace(keyword))
    {
        var searchKeyword = keyword.Trim().ToLower();
        query = query.Where(news =>
            news.Title.ToLower().Contains(searchKeyword) ||
            news.Description.ToLower().Contains(searchKeyword));
    }

    if (status.HasValue)
    {
        query = query.Where(news => news.Status == status.Value);
    }

    var totalCount = await query.CountAsync();
    var items = await query
        .OrderByDescending(news => news.CreatedAt)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .Select(news => new NewResponse
        {
            Id = news.Id,
            Title = news.Title,
            Description = news.Description,
            ViewCount = news.ViewCount,
            PictureUrl = news.CoverImage,
            slug = news.slug,
            Status = news.Status == NewsStatus.Published ? "Published" : "Draft",
            UserId = news.UserId,
            CategoryIds = news.CategoryNewsDetail.Select(categoryNewsDetail => categoryNewsDetail.CategoryNewsId).ToList(),
            CreatedAt = news.CreatedAt,
            UpdatedAt = news.UpdatedAt
        })
        .ToListAsync();

    return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
}
```

**Controller action (`Controller.cs`):**
```csharp
[HttpGet]
public async Task<IActionResult> GetNews([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string? keyword, [FromQuery] NewsStatus? status)
{
    var result = await _newsService.GetNewsAsync(pageIndex, pageSize, keyword, status);
    return Ok(result);
}
```

## Validation Policy

Always validate request properties on the DTO model class using standard **DataAnnotations** attributes:
- Required: `[Required(ErrorMessage = "FIELD_REQUIRED")]`
- Email: `[EmailAddress(ErrorMessage = "INVALID_EMAIL_FORMAT")]`
- Range: `[Range(1, int.MaxValue, ErrorMessage = "VALUE_MUST_BE_GREATER_THAN_ZERO")]`
- Compare: `[Compare(nameof(Password), ErrorMessage = "PASSWORD_CONFIRMATION_NOT_MATCH")]`

Do not use `IValidatableObject` or FluentValidation unless explicitly requested.

## Hard Rules

- Do not use verbs in standard CRUD route paths (e.g., avoid `/delete`, `/update`, `/get` inside resource paths).
- Do not create a new controller if an existing controller clearly owns the API.
- Do not skip `IService.cs` when adding service methods.
- Do not query the database or put business logic directly in controllers.
- Do not return EF entities directly; always map to a response DTO.
- Do not use `IValidatableObject` or FluentValidation for simple cross-field validations like password comparison when `[Compare]` works.

## Common Mistakes

| Mistake | Correct action |
| --- | --- |
| Using verbs in basic CRUD endpoints (e.g. `[HttpDelete("delete-event/{id}")]`) | Use RESTful noun routes and proper HTTP methods (e.g. `[HttpDelete("{id:guid}")]`) |
| Creating a custom `IValidatableObject` for password confirmation | Use the `[Compare(nameof(Password))]` DataAnnotation |
| Querying the database context inside a controller action | Always put queries in the Service implementation |
| Returning EF entities inside the pagination list | Project entities to response DTOs using `.Select()` before `.ToListAsync()` |
| Returning `Ok(ApiResponseFactory.BasePagination(...))` in the controller when the service returns `BasePaginationResponse` | Return `Ok(result)` directly |
