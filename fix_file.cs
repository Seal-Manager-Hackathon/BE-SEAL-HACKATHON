    public async Task<Models.BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, Models.PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();

        var myTeamIds = await _dbContext.TeamDetails
            .Where(x => x.UserId == userId && !x.IsDisable && x.Status == TeamDetailStatusEnum.Active)
            .Select(x => x.TeamId)
            .ToListAsync();

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            var allQuery = _dbContext.RegisterTeams
                .AsNoTracking()
                .Include(x => x.Team)
                .Include(x => x.Event)
                .Where(x => !x.IsDisable && myTeamIds.Contains(x.TeamId));

            var allTotalCount = await allQuery.CountAsync();

            var allItems = await allQuery
                .OrderBy(x => x.Status == RegisterTeamStatusEnum.Pending ? 0 : (x.Status == RegisterTeamStatusEnum.Approved ? 1 : 2))
                .ThenByDescending(x => x.CreatedAt)
                .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(x => new Response.RegisteredEventItemResponse
                {
                    RegisterId = x.Id,
                    TeamId = x.TeamId,
                    TeamName = x.Team.Name,
                    EventId = x.EventId,
                    EventName = x.Event.Name,
                    Status = x.Status.ToString()!,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return ApiResponseFactory.BasePagination(allItems, paginationRequest.PageIndex, paginationRequest.PageSize, allTotalCount);
        }

        var statusStr = request.Status.Trim();
        if (!Enum.TryParse<RegisterTeamStatusEnum>(statusStr, true, out var statusEnum))
        {
            throw new BadRequestException("INVALID_STATUS");
        }

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Event)
            .Where(x => !x.IsDisable && myTeamIds.Contains(x.TeamId) && x.Status == statusEnum);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.RegisteredEventItemResponse
            {
                RegisterId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Status = x.Status.ToString()!,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }
