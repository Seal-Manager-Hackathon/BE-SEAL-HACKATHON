---
name: create-api-skill
description: Use when adding or modifying API endpoints in the Hackathon .NET backend, especially when the user provides an API requirement, request shape, response needs, validation needs, or asks to follow existing Controller-Service patterns
---

# Create API Skill

## Overview

When creating an API in this repository, keep the endpoint aligned with the existing Hackathon backend structure: Controller action -> Service interface -> Service implementation -> Request/Response DTOs -> `ApiResponseFactory.Base(...)`.

The user will usually provide the API requirement and request shape. Treat the user's request shape as the source of truth. Infer the response DTO only when the required output is clear; otherwise ask a focused clarification before editing.

## User Input Contract

When the user asks for an API, expect these inputs:

- The API purpose or business action.
- The request shape, such as route params, query params, or body fields.
- Optional validation requirements.

Do not invent request fields. If the user gives the request but not the response, inspect the related service/entity patterns and propose the smallest response DTO that satisfies the API purpose. If more than one response shape would be reasonable, ask before editing.

## Required Workflow

1. Identify the target module from the user's requirement.
   - Prefer an existing controller/service folder when one matches the domain.
   - Create a new controller/service folder only when no existing module fits.
2. Inspect nearby examples before editing.
   - Use `GetMe` as a simple reference for Controller -> IService -> Service -> Response.
3. Define or update request DTOs in `Hackathon.Service/<Module>/Request.cs` when the API accepts body/query data.
   - Route-only parameters do not need a request class unless the API has multiple inputs or validation needs.
4. Define or update response DTOs in `Hackathon.Service/<Module>/Response.cs`.
   - Do not return EF entities directly when the API requires a shaped response.
   - Include only the fields required by the user or clearly needed by the endpoint.
5. Add the service contract to `Hackathon.Service/<Module>/IService.cs`.
6. Implement business logic in `Hackathon.Service/<Module>/Service.cs`.
   - Controllers should not query `AppDbContext` directly.
   - Use existing exception patterns from the project.
7. Add the controller action in the matching controller.
   - Return `Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier))` unless an existing endpoint pattern says otherwise.
   - Use route constraints such as `{id:guid}` for GUID route values.
8. Register DI in `Hackathon.Api/Program.cs` only when creating a new service module.
9. Build or run targeted checks after editing.

## Reference Pattern

Use these files as the baseline pattern:

- `Hackathon.Api/Controllers/AuthController.cs`: `GetMe` action calls `_authService.GetMe()` and wraps the result.
- `Hackathon.Service/Auth/IService.cs`: declares `Task<Response.GetMeResponse> GetMe()`.
- `Hackathon.Service/Auth/Response.cs`: declares `GetMeResponse` DTO.
- `Hackathon.Service/Auth/Service.cs`: implements `GetMe()` and projects database data into the response DTO.

## Validation Policy

Only add validation when the user asks for it or when the API requirement explicitly needs it.

Preferred order:

| Situation | Use |
| --- | --- |
| Simple required/email/length check already matching local style | DataAnnotations or existing request pattern |
| Cross-field validation in the request object | `IValidatableObject` when it keeps logic small |
| More complex rules or user explicitly asks for FluentValidation | FluentValidation validator |

If the user explicitly says to validate with FluentValidation, use FluentValidation for that request. If the user asks for `IValidatableObject` or the local pattern already uses it for similar cross-field checks, use `IValidatableObject`. Do not add a FluentValidation package or new validation infrastructure unless required for the requested API. Do not mix DataAnnotations, `IValidatableObject`, and FluentValidation for the same request unless the project already does so for that module.

## Clarify Before Editing

Ask one focused question before coding when any of these are unclear:

- Which module owns the endpoint.
- Whether the endpoint is public or requires `[Authorize]`.
- The response fields cannot be inferred from the user request.
- The domain rule is ambiguous, such as what qualifies a user as a mentor.
- Validation rules are not specified but could change behavior.

## Hard Rules

- Do not create a new controller if an existing controller clearly owns the API.
- Do not create a new service if an existing service module clearly owns the API.
- Do not skip `IService.cs` when adding service methods.
- Do not put business logic or database queries in controllers.
- Do not return repository entities directly for shaped API responses.
- Do not invent request fields the user did not provide.
- Do not ignore the user's requested validation style, especially FluentValidation or `IValidatableObject`.
- Do not add validation beyond the requested or necessary scope.
- Do not modify unrelated files.

## Common Mistakes

| Mistake | Correct action |
| --- | --- |
| Adding a controller action but forgetting `IService.cs` | Add the interface method first, then implement it |
| Creating a new module for an existing domain | Search existing controllers/services and extend the matching one |
| Returning `Users`, `Teams`, or other EF entities directly | Create a response DTO with the required fields |
| Guessing vague response fields | Ask for clarification or state the minimal inferred response before editing |
| Adding FluentValidation for every request | Add it only when requested or when rules are complex |
| Missing route constraints for GUID IDs | Use `{id:guid}` or equivalent route constraint |

## Red Flags

Stop and clarify or re-check project patterns if you think:

- "I'll just create a new controller to be safe."
- "The controller can query the database directly; it is faster."
- "The response can be the entity for now."
- "I'll add full validation even though the user did not ask."
- "The request is obvious even though the user did not specify fields."

These usually cause APIs that do not match this repository's conventions.
