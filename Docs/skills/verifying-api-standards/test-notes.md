# Verifying API Standards Skill Test Notes

## RED baseline

We simulated auditing code changes without the `verifying-api-standards` skill.

### Scenario: Auditing UsersController paged endpoint and ResetPasswordRequest DTO
- Developer submitted controller querying DB directly.
- Controller wrapped pagination response instead of Service wrapping it.
- Request DTO used `IValidatableObject` instead of DataAnnotations.

### Baseline behavior & gap:
- The baseline agent successfully flagged all 3 major violations in a textual analysis.
- However, it did not verify the service dependency registration in `Program.cs` or structural Guid template constraints on the Controller routes.
- The output did not follow a structured, standard report template.

## Skill requirements derived from baseline:
- Standardize the feedback format via a reporting template section.
- Explicitly add checks for Dependency Injection registration in `Program.cs`.
- Explicitly check for Guid route template constraints (e.g. `{id:guid}`).
- Enforce strict checks on transaction blocks for any state mutation (Create/Update/Delete).
- Explicitly check endpoint URLs for RESTful compliance (prohibit verbs in CRUD paths, enforce plural resource nouns).
