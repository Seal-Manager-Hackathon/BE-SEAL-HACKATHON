---
name: verifying-api-standards
description: Use when checking, auditing, or reviewing newly created or modified API endpoints, controllers, services, DTOs, or database transaction patterns in the Hackathon .NET backend
---

# Verifying API Standards Skill

## Overview

Use this skill to audit code modifications, pull requests, or newly created API endpoints to ensure they conform perfectly to the project's standards.

## Auditing Checklist

When auditing API changes, verify each of the following points:

### 1. Controller Layer Standards
- **Inheritance & Attributes**: The controller must use `[ApiController]` and route template matching the module.
- **Pattern Matching**: The controller template must match `AuthController.cs` (constructor injection of service interface, endpoints call service methods).
- **Direct Queries**: The controller **must NOT** query the database or DbContext directly.
- **Response Wrapper**: 
  - Regular APIs: Action must wrap the service result in `ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier)`.
  - Paginated APIs: Action must return `Ok(result)` directly.
- **GUID Constraints**: Any route parameter representing GUID must include the `:guid` constraint (e.g. `[HttpGet("{id:guid}")]`).

### 2. Service & Interface Layer Standards
- **Interface Segregation**: Every service method must be declared in `IService.cs` before implementing it in `Service.cs`.
- **Database Context Usage**: The database context must be injected and queried ONLY inside the Service layer.
- **Exceptions**: Use existing project-defined exception classes (e.g. `NotFoundException`, `BadRequestException`, `ConflictException`) instead of generic exceptions.
- **Transactions**: Any state mutation (Create, Update, Delete) or multi-step database action **must be wrapped in a database transaction** using `await _dbContext.Database.BeginTransactionAsync()` inside the Service layer.

### 3. Request & Response DTOs
- **Entity Exposure**: Never return Entity Framework database models directly to the controller or client. Always project them to a specific DTO in `Response.cs`.
- **DataAnnotations Validation**: Request validation must be performed directly on DTO model properties in `Request.cs` using **DataAnnotations** (e.g. `[Required]`, `[EmailAddress]`, `[Range]`, `[Compare]`).
- **No IValidatableObject/FluentValidation**: Do not use `IValidatableObject` or FluentValidation unless explicitly requested. Cross-field validations like password comparison must use `[Compare]`.

### 4. Pagination Standards
- **Return Type**: The service method for a paginated query must return `Task<BasePaginationResponse>` directly.
- **Page Limits**: Ensure pagination parameters are sanitized:
  ```csharp
  pageIndex = pageIndex <= 0 ? 1 : pageIndex;
  pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);
  ```
- **Skip & Take implementation**: Use `.Skip((pageIndex - 1) * pageSize).Take(pageSize)` correctly after retrieving the total count.
- **Factory Return**: The service must wrap and return the paged result via:
  ```csharp
  return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
  ```

---

## Standard Reference Files

Use these files in the repository to compare patterns:
- **Controller Template**: `Hackathon.Api/Controllers/AuthController.cs`
- **Paging Implementation**: `Hackathon.Service/Tracks/Service.cs` (GetTracks method)
- **Validation DTO**: `Hackathon.Service/Auth/Request.cs` (ResetPasswordRequest class)

---

## Common Violations and How to Suggest Fixes

| Violation Symptom | Correct Suggestion / Action |
| --- | --- |
| Controller calling `_dbContext.Users` or `_dbContext.EmailVerifications` | Instruct to move query logic to `Service.cs` and inject service interface in controller |
| Controller wrapping paginated response in `BasePagination` again | Change controller return to `Ok(result)` and wrap inside the Service instead |
| Request DTO using `IValidatableObject` to compare passwords | Replace with `[Compare(nameof(Password), ErrorMessage = "...")]` attribute |
| Service method mutating DB state without `BeginTransactionAsync()` | Wrap the save operations in `try-catch` block with transaction commit & rollback |
| Route parameter `[HttpGet("events/{eventId}")]` missing guid check | Suggest changing to `[HttpGet("events/{eventId:guid}")]` |
| Service registering is missing in DI | Add `builder.Services.AddScoped<IService, Service>();` in `Program.cs` |

## Reporting Template

When auditing, structure your feedback using this format:

1. **Overview**: Summary of the changes and general assessment.
2. **Violations Found**: Bulleted list of violations grouped by layer (Controller, Service, DTO). Detail exactly what is wrong and what is missing.
3. **DI Registrations Check**: Verify if the service needs registration in `Program.cs`.
4. **Corrected Code**: Provide code snippets showing exactly how the files must be written to follow standard.
