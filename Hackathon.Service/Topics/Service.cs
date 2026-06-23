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

    public async Task<Response.TopicDetailResponse> GetTopicDetail(Guid topicId)
    {
        var topic = await _dbContext.Topics
            .AsNoTracking()
            .Where(x => x.Id == topicId && !x.IsDisable)
            .Select(x => new Response.TopicDetailResponse
            {
                Id = x.Id,
                TrackId = x.TrackId,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (topic == null)
        {
            throw new NotFoundException("TOPIC_NOT_FOUND");
        }

        return topic;
    }

    public async Task<Response.CreateTopicResponse> CreateTopic(Guid trackId, Request.CreateTopicRequest request)
    {
        // Verify track exists
        var trackExists = await _dbContext.Tracks.AnyAsync(x => x.Id == trackId && !x.IsDisable);
        if (!trackExists)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        // Check for duplicate title within the same track
        var isDuplicate = await _dbContext.Topics
            .AnyAsync(x => x.TrackId == trackId && x.Title == request.Title && !x.IsDisable);
            
        if (isDuplicate)
        {
            throw new ConflictException("TOPIC_TITLE_ALREADY_EXISTS");
        }

        var newTopic = new Hackathon.Repository.Entity.Topics
        {
            Id = Guid.NewGuid(),
            TrackId = trackId,
            Title = request.Title,
            Description = request.Description,
            IsDisable = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.Topics.Add(newTopic);
        await _dbContext.SaveChangesAsync();

        return new Response.CreateTopicResponse
        {
            Id = newTopic.Id,
            TrackId = newTopic.TrackId,
            Title = newTopic.Title,
            Description = newTopic.Description,
            IsDisable = newTopic.IsDisable,
            CreatedAt = newTopic.CreatedAt,
            UpdatedAt = newTopic.UpdatedAt
        };
    }

    public async Task<string> UpdateTopic(Guid topicId, Request.UpdateTopicRequest request)
    {
        var topic = await _dbContext.Topics.FirstOrDefaultAsync(x => x.Id == topicId && !x.IsDisable);
        
        if (topic == null)
        {
            throw new NotFoundException("TOPIC_NOT_FOUND");
        }

        // Check if new title conflicts with an existing topic in the same track
        if (topic.Title != request.Title)
        {
            var isDuplicate = await _dbContext.Topics
                .AnyAsync(x => x.TrackId == topic.TrackId && x.Title == request.Title && x.Id != topicId && !x.IsDisable);
                
            if (isDuplicate)
            {
                throw new ConflictException("TOPIC_TITLE_ALREADY_EXISTS");
            }
        }

        topic.Title = request.Title;
        topic.Description = request.Description;
        topic.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Topics.Update(topic);
        await _dbContext.SaveChangesAsync();

        return "TOPIC_UPDATED_SUCCESSFULLY";
    }

    public async Task<string> DeleteTopic(Guid topicId)
    {
        var topic = await _dbContext.Topics.FirstOrDefaultAsync(x => x.Id == topicId && !x.IsDisable);
        
        if (topic == null)
        {
            throw new NotFoundException("TOPIC_NOT_FOUND");
        }

        topic.IsDisable = true;
        topic.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Topics.Update(topic);
        await _dbContext.SaveChangesAsync();

        return "TOPIC_DELETED_SUCCESSFULLY";
    }
}
