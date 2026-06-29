using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Criticals;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response.RoundCriteriaResponse> GetCriteriaByRound(Guid roundId)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .Include(x => x.Event)
            .Where(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable)
            .Select(x => new
            {
                x.Id,
                x.EventId,
                x.Name,
            })
            .FirstOrDefaultAsync();

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var criteriaTemplate = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId && x.IsDisable)
            .Select(x => new Response.CriteriaTemplateResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
                Items = x.CriteriaItems
                    .Where(item => !item.IsDisable)
                    .OrderBy(item => item.CreatedAt)
                    .ThenBy(item => item.Name)
                    .Select(item => new Response.CriteriaItemResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Score = item.Score,
                        IsDisable = item.IsDisable,
                        CreatedAt = item.CreatedAt,
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync();

        return new Response.RoundCriteriaResponse
        {
            RoundId = round.Id,
            EventId = round.EventId,
            RoundName = round.Name,
            Template = criteriaTemplate,
        };
    }

    public async Task<Response.CreateCriteriaResponse> CreateCriteria(Guid eventId, Guid roundId, Request.CreateCriteriaRequest request)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
            throw new NotFoundException("EVENT_NOT_FOUND");

        var round = await _dbContext.Rounds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roundId && x.EventId == eventId && !x.IsDisable);

        if (round == null)
            throw new NotFoundException("ROUND_NOT_FOUND");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new BadRequestException("CRITERIA_TITLE_REQUIRED");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var template = new Repository.Entity.CriteriaTemplates
        {
            Id = Guid.NewGuid(),
            RoundId = roundId,
            Title = request.Title,
            Description = request.Description,
            IsDisable = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.CriteriaTemplates.Add(template);

        if (request.Items.Count != 0)
        {
            var items = request.Items.Select(item => new Repository.Entity.CriteriaItems
            {
                Id = Guid.NewGuid(),
                CriteriaTemplateId = template.Id,
                Name = item.Name,
                Description = item.Description,
                Score = item.Score,
                IsDisable = false,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }).ToList();

            _dbContext.CriteriaItems.AddRange(items);
        }

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return new Response.CreateCriteriaResponse
        {
            Id = template.Id
        };
    }

    public async Task ActivateCriteria(Guid eventId, Guid roundId, Guid templateId)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
            throw new NotFoundException("EVENT_NOT_FOUND");

        var round = await _dbContext.Rounds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roundId && x.EventId == eventId && !x.IsDisable);

        if (round == null)
            throw new NotFoundException("ROUND_NOT_FOUND");

        var template = await _dbContext.CriteriaTemplates
            .FirstOrDefaultAsync(x => x.Id == templateId && x.RoundId == roundId);

        if (template == null)
            throw new NotFoundException("CRITERIA_TEMPLATE_NOT_FOUND");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        // Deactivate all active templates of this round
        var allTemplates = await _dbContext.CriteriaTemplates
            .Where(x => x.RoundId == roundId && x.IsDisable)
            .ToListAsync();

        foreach (var t in allTemplates)
        {
            t.IsDisable = false;
            t.UpdatedAt = DateTimeOffset.UtcNow;
        }

        // Activate the selected template
        template.IsDisable = true;
        template.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<List<Response.CriteriaTemplateResponse>> GetCriteriaTemplatesByRound(Guid eventId, Guid roundId)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
            throw new NotFoundException("EVENT_NOT_FOUND");

        var round = await _dbContext.Rounds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roundId && x.EventId == eventId && !x.IsDisable);

        if (round == null)
            throw new NotFoundException("ROUND_NOT_FOUND");

        var templates = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new Response.CriteriaTemplateResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
                Items = x.CriteriaItems
                    .OrderBy(item => item.CreatedAt)
                    .ThenBy(item => item.Name)
                    .Select(item => new Response.CriteriaItemResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Score = item.Score,
                        IsDisable = item.IsDisable,
                        CreatedAt = item.CreatedAt,
                    })
                    .ToList(),
            })
            .ToListAsync();

        return templates;
    }

    public async Task<List<Response.RoundCriteriaResponse>> GetCriteriaByEvent(Guid eventId)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var rounds = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .OrderBy(x => x.RoundNo)
            .Select(x => new
            {
                x.Id,
                x.EventId,
                x.Name,
            })
            .ToListAsync();

        var roundIds = rounds.Select(r => r.Id).ToList();

        var criteriaTemplates = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => roundIds.Contains(x.RoundId) && x.IsDisable)
            .Select(x => new
            {
                x.RoundId,
                Template = new Response.CriteriaTemplateResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsDisable = x.IsDisable,
                    CreatedAt = x.CreatedAt,
                    Items = x.CriteriaItems
                        .Where(item => !item.IsDisable)
                        .OrderBy(item => item.CreatedAt)
                        .ThenBy(item => item.Name)
                        .Select(item => new Response.CriteriaItemResponse
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            Score = item.Score,
                            IsDisable = item.IsDisable,
                            CreatedAt = item.CreatedAt,
                        })
                        .ToList(),
                }
            })
            .ToListAsync();

        var templateDict = criteriaTemplates
            .GroupBy(x => x.RoundId)
            .ToDictionary(g => g.Key, g => g.FirstOrDefault()?.Template);

        var result = new List<Response.RoundCriteriaResponse>();
        foreach (var round in rounds)
        {
            templateDict.TryGetValue(round.Id, out var template);
            result.Add(new Response.RoundCriteriaResponse
            {
                RoundId = round.Id,
                EventId = round.EventId,
                RoundName = round.Name,
                Template = template
            });
        }

        return result;
    }
}
