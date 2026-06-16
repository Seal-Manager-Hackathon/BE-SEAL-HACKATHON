# Create API Skill Test Notes

## RED baseline

Initial parallel baseline attempts failed due API rate limits, so one scenario was rerun sequentially.

### Scenario: mentor profile by mentorId

Prompt asked for an API that receives `mentorId` from route and returns `fullName`, `email`, `avatar`.

Baseline behavior produced a detailed plan but exposed ambiguities and risks the skill must prevent:

- It created a new `Mentors` module without first proving whether an existing module should own mentor profile APIs.
- It inferred mentor identity rules from AssignEvents/EventRoles, which may be wrong without clarification.
- It noted `[Authorize]` ambiguity but did not require clarification before implementation.
- It correctly identified response DTO, route GUID constraint, service interface, service implementation, controller, and DI registration for a new module.

## Skill requirements derived from baseline

- Force module ownership check before creating a new controller/service.
- Force clarification for ambiguous domain rules such as what qualifies a mentor.
- Force clarification for public vs authorized endpoints when unclear.
- Require response DTO instead of returning entities.
- Require route constraints for GUID IDs.
- Keep validation scoped to user request.
