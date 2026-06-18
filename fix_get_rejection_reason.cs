    public async Task<Response.RejectionReasonResponse> GetRejectionReason(Guid registerId)
    {
        var userId = GetCurrentUserId();

        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        // Check if user is in the team
        var isMember = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == registerTeam.TeamId && x.UserId == userId && !x.IsDisable && x.Status == TeamDetailStatusEnum.Active);
        if (!isMember)
        {
            throw new ForbiddenException("USER_NOT_IN_TEAM");
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Pending)
        {
            return new Response.RejectionReasonResponse
            {
                RegisterId = registerTeam.Id,
                Status = registerTeam.Status.ToString()!,
                RejectionReason = "Đang đợi xét duyệt"
            };
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Approved)
        {
            return new Response.RejectionReasonResponse
            {
                RegisterId = registerTeam.Id,
                Status = registerTeam.Status.ToString()!,
                RejectionReason = "Đã được đồng ý"
            };
        }

        return new Response.RejectionReasonResponse
        {
            RegisterId = registerTeam.Id,
            Status = registerTeam.Status.ToString()!,
            RejectionReason = registerTeam.RejectionReason
        };
    }
