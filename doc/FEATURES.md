# Main Features

## Account & Auth
- Register/login/logout.
- Refresh token.
- Reset password.
- Email verification.
- User status/ban handling.

## User Profile
- View profile (`GET /api/v1/user/me`).
- Update profile (`PATCH /api/v1/user/me`) — fields: firstName, lastName, phoneNumber, bio, address, dateOfBirth, studentId (set once only), imgUrl, linkUrl. Cannot change AvatarUrl or College.
- Validate required student profile fields.
- Store profile fields in `Users`.

## Role & Assignment
- Manage global role through `Users.Role` (`RoleEnum`); no separate Roles/UserRoles tables.
- Assign staff/lecture to event via `AssignEvents`.
- Assign mentor/judge to track via `AssignTracks`.

## Event Management
- Create/update event.
- Configure registration time, team limits, member limits, status, season.
- Setup validation before publish (rounds, criteria, tracks, topics, awards, staff).
- Auto-create leaderboard on publish.
- Configure awards and leaderboard.

## Round & Criteria
- Create/update rounds.
- Configure submission window.
- Auto-update Event.NumberRound on round create/delete.
- Create criteria templates and criteria items.

## Track & Topic
- Create tracks in event.
- Create topics in track.
- Record offline draw result by assigning topic to registered team.

## Team Management
- Create team.
- Invite/accept/reject member (max 50).
- Kick member (hard delete TeamDetails record).
- Leave team (soft-disable member).
- Disband team (soft-disable all members + team).

## Registration
- Team leader registers team for event/topic.
- Staff approves/rejects team.
- Staff bans/unbans team with RejectionReason.
- Auto-reject pending registrations when RegisterLimitTime passes.

## Round Participation
- Use `RoundDetails` to place registered teams into rounds.
- Track advancement/stopped teams via service/status rules.
- Admin can manually advance team to next round.
- Any authenticated user can check team's current round.

## Submission
- Team leader submits work for round.
- Support multiple submissions by multiple records.
- Judge/staff use latest valid submission before deadline.
- Submission status: Submitted, Graded, Failed.

## Judging & Scoring
- Judge views assigned track submissions.
- Judge scores by criteria item (all items required, no partial submission).
- Upsert pattern: if judge already scored, soft-delete old items and recreate.
- Store totals in `Scores`, details in `ScoreItems`.
- TotalScore = SUM of all ScoreItems.Score.
- Retake score: only allowed when Submission.IsRegrade == true and approved report exists.
- Setting score auto-updates Submission.Status to Graded.

## Report & Regrade
- User sends report (no FK to AssignEvent or Submission).
- Report statuses: Pending, Resolved, Reject, Canceled.
- Staff approves/rejects regrade requests.
- ApproveRegrade sets Report.Status=Resolved, Submission.IsRegrade=true.

## Leaderboard
- Event leaderboard and details by team.
- Year leaderboard by aggregating event leaderboard scores.
- Auto-created on event publish.

## Notification
- General notifications via `Notifications`.
- Mentor notices via `MentorNotifications`.

## Background Jobs (Quartz)
- Auto-close expired events (every 10 min).
- Auto-reject pending registrations (every 12 hours).
- Expire pending email verifications (every 2 min).
- Expire pending invitations (every 15 min).

## Admin/System
- User management and global role updates via `Users.Role`.
- Assignment management.
- Event lifecycle control.
- Operational reports and manual decisions.
