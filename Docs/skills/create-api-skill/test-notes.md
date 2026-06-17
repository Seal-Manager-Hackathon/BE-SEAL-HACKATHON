# Create API Skill Test Notes

## RED baseline

Initial parallel baseline attempts failed due to API rate limits, so one scenario was rerun sequentially.

### Scenario: mentor profile by mentorId

Prompt asked for an API that receives `mentorId` from route and returns `fullName`, `email`, `avatar`.

Baseline behavior produced a detailed plan but exposed ambiguities and risks the skill must prevent:
- It created a new `Mentors` module without first checking if an existing module should own mentor profile APIs.
- It did not clarify `[Authorize]` vs public endpoints before implementation.
- It correctly identified response DTO mapping, route GUID constraint, service interface, service implementation, controller, and DI registration.

## Skill requirements derived from baseline & updates

- **Controller pattern**: If a controller does not exist for a module, create a new one modeled after `AuthController.cs`.
- **Validation**: Enforce request DTO validation using **DataAnnotations** (e.g. `[Compare]`, `[Range]`, `[Required]`). Do not use `IValidatableObject` or FluentValidation unless explicitly requested.
- **Pagination**: If pagination is required, the Service should return `BasePaginationResponse` directly via `ApiResponseFactory.BasePagination(...)` and the Controller should return `Ok(result)` directly.
- **Response mapping**: Always ask the user for response requirements first, or propose a concrete response DTO based on the description and the related entities before writing code. Do not return database entities directly.
