using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class CriteriaSeed
{
    public static void SeedCriteria(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CriteriaTemplates>().HasData(
            new CriteriaTemplates
            {
                Id = SeedConstants.IdeaCriteriaTemplateId,
                RoundId = SeedConstants.IdeaRoundId,
                Title = "Idea Evaluation Template",
                Description = "Criteria for idea validation",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new CriteriaTemplates
            {
                Id = SeedConstants.FinalCriteriaTemplateId,
                RoundId = SeedConstants.FinalRoundId,
                Title = "Final Demo Evaluation Template",
                Description = "Criteria for final demo",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );

        modelBuilder.Entity<CriteriaItems>().HasData(
            new CriteriaItems
            {
                Id = SeedConstants.InnovationCriteriaItemId,
                CriteriaTemplateId = SeedConstants.IdeaCriteriaTemplateId,
                Name = "Innovation",
                Description = "Novelty of the idea",
                Score = 40m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new CriteriaItems
            {
                Id = SeedConstants.FeasibilityCriteriaItemId,
                CriteriaTemplateId = SeedConstants.IdeaCriteriaTemplateId,
                Name = "Feasibility",
                Description = "Feasibility of execution",
                Score = 60m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new CriteriaItems
            {
                Id = SeedConstants.TechnicalCriteriaItemId,
                CriteriaTemplateId = SeedConstants.FinalCriteriaTemplateId,
                Name = "Technical Execution",
                Description = "Quality of technical implementation",
                Score = 50m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new CriteriaItems
            {
                Id = SeedConstants.PresentationCriteriaItemId,
                CriteriaTemplateId = SeedConstants.FinalCriteriaTemplateId,
                Name = "Presentation",
                Description = "Clarity of presentation",
                Score = 50m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
