# Business & Authorization Rules

## Authorization
- No dynamic Permission/RBAC tables.
- API authorization is hard-coded by role/action.
- Global roles: `Admin`, `Staff`, `Student`, `Lecturer`.
- Event roles: `Mentor`, `Judge`.
- Event/track APIs must check global role + `AssignEvents` + `AssignTracks` + business rule.

## Account/Profile
- Student must complete profile before creating/joining a team.
- Profile fields live in `Users`; no `Profile` table.
- Banned/disabled user cannot log in.
- Unverified email login: disable old verifications, send new verification email, block with EMAIL_UNVERIFIED_OTP_SENT.
- Refresh token rotation: check access token is actually expired, revoke old token, issue new pair.
- Default avatar: `https://robohash.org/{email}`. Default college: "FPT University".

## Team/Register
- Team creator is leader.
- Team leader submits event registration.
- Staff approves/rejects whole teams, not individual members.
- A student cannot join multiple teams in the same event.
- Member count must satisfy `Events.MinMember` and `Events.MaxMember`.
- Max 50 members per team on invite.
- Approved team registration locks members for that event flow.
- Kick member = hard delete TeamDetails record.
- Leave team = soft-disable member; leader cannot leave.
- Disband team = soft-disable all members + team.

## Event/Round/Criteria
- Admin creates event and event setup.
- Event has many rounds; NumberRound is auto-calculated.
- Publish validation: has rounds, criteria, tracks, topics, awards, staff.
- Published → Draft: NOT allowed.
- Draft → Closed: NOT allowed directly.
- Only Published → Closed allowed.
- Leaderboard auto-created on publish.
- Round has many criteria templates.
- Criteria template has many criteria items.
- Criteria should not be changed freely after scoring starts.

## Track/Topic/Draw
- Track belongs to event.
- Topic belongs to track.
- Track/topic draw is offline; staff records result.
- Track is inferred by `RegisterTeams.TopicId -> Topics.TrackId`.
- `RoundDetails` connects `Rounds` and `RegisterTeams`.

## Mentor/Judge
- Lecturer becomes mentor/judge only through assignment.
- Mentor supports and sends notices; mentor does not score.
- Judge scores assigned track submissions by criteria.
- A lecture should not be mentor and judge in the same event.

## Submission/Score
- Team leader submits official submissions.
- A team can submit multiple times; use latest valid submission before deadline.
- One submission can be scored by multiple judges.
- Score submission requires ALL criteria items (no partial).
- Score upsert: if judge already scored, soft-delete old items and create new.
- TotalScore = SUM of ScoreItems.Score.
- Grading auto-sets Submission.Status = Graded.
- Retake: requires Submission.IsRegrade == true and approved "Phúc khảo" report.
- Score edits update existing record before finalized; no score version table.

## Report/Regrade
- Reports only belong to User (no FK to AssignEvent or Submission).
- Report status machine: Pending → Resolved | Reject | Canceled.
- Only Pending reports can be modified.
- Staff handles report/regrade manually.
- Regrade result is final.

## Leaderboard
- Event leaderboard = total round scores.
- Year leaderboard = total event leaderboard scores.
- `LeaderBoardDetails.LevelAward` stores award level/result.
- Leaderboard auto-created with Year=EndTime.Year on event publish.

## Background Jobs
- AutoCloseExpiredEventsJob: runs every 10 min, closes events past EndTime.
- AutoRejectPendingRegistrationsJob: runs every 12 hours, rejects pending registrations past RegisterLimitTime.
- ExpirePendingEmailVerificationsJob: runs every 2 min.
- ExpirePendingInvitationsJob: runs every 15 min.

## Audit
- No `AuditLogs` table.
- Important actions logged at service/app level if needed.
