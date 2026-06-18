using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Topics;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response.AssignedTopicResponse> GetTopic(Guid eventId, Guid registerTeamId)
    {
        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Track)
            .Include(x => x.Topic)
            .Where(x => x.Id == registerTeamId && x.EventId == eventId)
            .Select(x => new Response.AssignedTopicResponse
            {
                RegisterTeamId = x.Id,
                EventId = x.EventId,
                TrackId = x.TrackId,
                TrackTitle = x.Track != null ? x.Track.Title : null,
                TrackDescription = x.Track != null ? x.Track.Description : null,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                TopicDescription = x.Topic != null ? x.Topic.Description : null
            })
            .FirstOrDefaultAsync();

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        return registerTeam;
    }
}
