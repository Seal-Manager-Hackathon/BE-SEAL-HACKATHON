using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Track IDs: 24000000-xxxx, Topic IDs: 25000000-xxxx
public static class TrackSeed
{
    public static readonly Guid Track1 = Guid.Parse("24000000-0000-0000-0000-000000000001");
    public static readonly Guid Track2 = Guid.Parse("24000000-0000-0000-0000-000000000002");
    public static readonly Guid Track3 = Guid.Parse("24000000-0000-0000-0000-000000000003");
    public static readonly Guid Track4 = Guid.Parse("24000000-0000-0000-0000-000000000004");
    public static readonly Guid Track5 = Guid.Parse("24000000-0000-0000-0000-000000000005");
    public static readonly Guid Track6 = Guid.Parse("24000000-0000-0000-0000-000000000006");
    public static readonly Guid Track7 = Guid.Parse("24000000-0000-0000-0000-000000000007");
    public static readonly Guid Track8 = Guid.Parse("24000000-0000-0000-0000-000000000008");
    public static readonly Guid Track9 = Guid.Parse("24000000-0000-0000-0000-000000000009");
    public static readonly Guid Track10 = Guid.Parse("24000000-0000-0000-0000-000000000010");
    public static readonly Guid Track11 = Guid.Parse("24000000-0000-0000-0000-000000000011");
    public static readonly Guid Track12 = Guid.Parse("24000000-0000-0000-0000-000000000012");
    public static readonly Guid Track13 = Guid.Parse("24000000-0000-0000-0000-000000000013");
    public static readonly Guid Track14 = Guid.Parse("24000000-0000-0000-0000-000000000014");
    public static readonly Guid Track15 = Guid.Parse("24000000-0000-0000-0000-000000000015");
    public static readonly Guid Track16 = Guid.Parse("24000000-0000-0000-0000-000000000016");
    public static readonly Guid Track17 = Guid.Parse("24000000-0000-0000-0000-000000000017");
    public static readonly Guid Track18 = Guid.Parse("24000000-0000-0000-0000-000000000018");
    public static readonly Guid Track19 = Guid.Parse("24000000-0000-0000-0000-000000000019");
    public static readonly Guid Track20 = Guid.Parse("24000000-0000-0000-0000-000000000020");
    public static readonly Guid Track21 = Guid.Parse("24000000-0000-0000-0000-000000000021");
    public static readonly Guid Track22 = Guid.Parse("24000000-0000-0000-0000-000000000022");
    public static readonly Guid Track23 = Guid.Parse("24000000-0000-0000-0000-000000000023");
    public static readonly Guid Track24 = Guid.Parse("24000000-0000-0000-0000-000000000024");
    public static readonly Guid Track25 = Guid.Parse("24000000-0000-0000-0000-000000000025");
    public static readonly Guid Track26 = Guid.Parse("24000000-0000-0000-0000-000000000026");
    public static readonly Guid Track27 = Guid.Parse("24000000-0000-0000-0000-000000000027");
    public static readonly Guid Track28 = Guid.Parse("24000000-0000-0000-0000-000000000028");
    public static readonly Guid Track29 = Guid.Parse("24000000-0000-0000-0000-000000000029");
    public static readonly Guid Track30 = Guid.Parse("24000000-0000-0000-0000-000000000030");

    public static readonly Guid Topic1 = Guid.Parse("25000000-0000-0000-0000-000000000001");
    public static readonly Guid Topic2 = Guid.Parse("25000000-0000-0000-0000-000000000002");
    public static readonly Guid Topic3 = Guid.Parse("25000000-0000-0000-0000-000000000003");
    public static readonly Guid Topic4 = Guid.Parse("25000000-0000-0000-0000-000000000004");
    public static readonly Guid Topic5 = Guid.Parse("25000000-0000-0000-0000-000000000005");
    public static readonly Guid Topic6 = Guid.Parse("25000000-0000-0000-0000-000000000006");
    public static readonly Guid Topic7 = Guid.Parse("25000000-0000-0000-0000-000000000007");
    public static readonly Guid Topic8 = Guid.Parse("25000000-0000-0000-0000-000000000008");
    public static readonly Guid Topic9 = Guid.Parse("25000000-0000-0000-0000-000000000009");
    public static readonly Guid Topic10 = Guid.Parse("25000000-0000-0000-0000-000000000010");
    public static readonly Guid Topic11 = Guid.Parse("25000000-0000-0000-0000-000000000011");
    public static readonly Guid Topic12 = Guid.Parse("25000000-0000-0000-0000-000000000012");
    public static readonly Guid Topic13 = Guid.Parse("25000000-0000-0000-0000-000000000013");
    public static readonly Guid Topic14 = Guid.Parse("25000000-0000-0000-0000-000000000014");
    public static readonly Guid Topic15 = Guid.Parse("25000000-0000-0000-0000-000000000015");
    public static readonly Guid Topic16 = Guid.Parse("25000000-0000-0000-0000-000000000016");
    public static readonly Guid Topic17 = Guid.Parse("25000000-0000-0000-0000-000000000017");
    public static readonly Guid Topic18 = Guid.Parse("25000000-0000-0000-0000-000000000018");
    public static readonly Guid Topic19 = Guid.Parse("25000000-0000-0000-0000-000000000019");
    public static readonly Guid Topic20 = Guid.Parse("25000000-0000-0000-0000-000000000020");
    public static readonly Guid Topic21 = Guid.Parse("25000000-0000-0000-0000-000000000021");
    public static readonly Guid Topic22 = Guid.Parse("25000000-0000-0000-0000-000000000022");
    public static readonly Guid Topic23 = Guid.Parse("25000000-0000-0000-0000-000000000023");
    public static readonly Guid Topic24 = Guid.Parse("25000000-0000-0000-0000-000000000024");
    public static readonly Guid Topic25 = Guid.Parse("25000000-0000-0000-0000-000000000025");
    public static readonly Guid Topic26 = Guid.Parse("25000000-0000-0000-0000-000000000026");
    public static readonly Guid Topic27 = Guid.Parse("25000000-0000-0000-0000-000000000027");
    public static readonly Guid Topic28 = Guid.Parse("25000000-0000-0000-0000-000000000028");
    public static readonly Guid Topic29 = Guid.Parse("25000000-0000-0000-0000-000000000029");
    public static readonly Guid Topic30 = Guid.Parse("25000000-0000-0000-0000-000000000030");

    public static void SeedTracks(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;
        // 30 Tracks across 7 events (E2, E3, E4, E6, E7, E9, E10) — 4-5 each + extras
        modelBuilder.Entity<Tracks>().HasData(
            // E2 (Published) — 5 tracks
            Create(Track1, SeedConstants.Event2Published, "Artificial Intelligence", "AI & Machine Learning", 5),
            Create(Track2, SeedConstants.Event2Published, "Web Development", "Full-stack web apps", 4),
            Create(Track3, SeedConstants.Event2Published, "Mobile App", "Mobile applications", 3),
            Create(Track4, SeedConstants.Event2Published, "Blockchain", "Blockchain & Web3", 3),
            Create(Track5, SeedConstants.Event2Published, "IoT", "Internet of Things", 3),
            // E4 (Published) — 5 tracks
            Create(Track6, SeedConstants.Event4Published, "Green Tech", "Environment & sustainability", 5),
            Create(Track7, SeedConstants.Event4Published, "FinTech", "Financial technology", 4),
            Create(Track8, SeedConstants.Event4Published, "EdTech", "Education technology", 4),
            Create(Track9, SeedConstants.Event4Published, "HealthTech", "Healthcare technology", 4),
            Create(Track10, SeedConstants.Event4Published, "Smart City", "Urban solutions", 4),
            // E7 (Published) — 4 tracks
            Create(Track11, SeedConstants.Event7Published, "Cyber Security", "Security & privacy", 4),
            Create(Track12, SeedConstants.Event7Published, "Data Science", "Data analytics & AI", 4),
            Create(Track13, SeedConstants.Event7Published, "Cloud Computing", "Cloud native apps", 3),
            Create(Track14, SeedConstants.Event7Published, "DevOps", "CI/CD & automation", 3),
            // E10 (Published) — 4 tracks
            Create(Track15, SeedConstants.Event10Published, "Game Development", "Game design & dev", 4),
            Create(Track16, SeedConstants.Event10Published, "AR/VR", "Augmented & virtual reality", 3),
            Create(Track17, SeedConstants.Event10Published, "Social Impact", "Tech for social good", 3),
            Create(Track18, SeedConstants.Event10Published, "Open Innovation", "Any innovative idea", 4),
            // E3 (Closed) — 3 tracks
            Create(Track19, SeedConstants.Event3Closed, "AI Legacy", "AI track from past event", 3),
            Create(Track20, SeedConstants.Event3Closed, "Web Legacy", "Web development past", 3),
            Create(Track21, SeedConstants.Event3Closed, "Mobile Legacy", "Mobile dev past", 2),
            // E6 (Closed) — 2 tracks
            Create(Track22, SeedConstants.Event6Closed, "Summer AI", "AI summer track", 3),
            Create(Track23, SeedConstants.Event6Closed, "Summer Web", "Web summer track", 3),
            // E9 (Closed) — 2 tracks
            Create(Track24, SeedConstants.Event9Closed, "Winter AI", "AI winter track", 3),
            Create(Track25, SeedConstants.Event9Closed, "Winter Web", "Web winter track", 3),
            // Disabled tracks
            Create(Track26, SeedConstants.Event2Published, "Legacy Track", "Disabled track in E2", 0, true),
            Create(Track27, SeedConstants.Event4Published, "Old Track E4", "Disabled in E4", 0, true),
            Create(Track28, SeedConstants.Event7Published, "Old Track E7", "Disabled in E7", 0, true),
            Create(Track29, SeedConstants.Event10Published, "Old Track E10", "Disabled in E10", 0, true),
            Create(Track30, SeedConstants.Event3Closed, "Old Track E3", "Disabled in E3", 0, true)
        );

        // 30 Topics (1 per track)
        modelBuilder.Entity<Topics>().HasData(
            Create(Topic1, Track1, "AI Chatbot", "Build intelligent chatbot"),
            Create(Topic2, Track2, "E-commerce Platform", "Full-stack e-commerce"),
            Create(Topic3, Track3, "Fitness App", "Mobile fitness tracking"),
            Create(Topic4, Track4, "NFT Marketplace", "NFT trading platform"),
            Create(Topic5, Track5, "Smart Home", "IoT smart home system"),
            Create(Topic6, Track6, "Carbon Tracker", "Track carbon footprint"),
            Create(Topic7, Track7, "Digital Wallet", "E-wallet solution"),
            Create(Topic8, Track8, "Learning Platform", "Online learning"),
            Create(Topic9, Track9, "Telemedicine", "Remote healthcare"),
            Create(Topic10, Track10, "Traffic Management", "Smart traffic system"),
            Create(Topic11, Track11, "Vulnerability Scanner", "Security scanning tool"),
            Create(Topic12, Track12, "Data Dashboard", "Analytics dashboard"),
            Create(Topic13, Track13, "Cloud Migration", "Cloud migration tool"),
            Create(Topic14, Track14, "CI/CD Pipeline", "Automation pipeline"),
            Create(Topic15, Track15, "2D Platformer", "2D game development"),
            Create(Topic16, Track16, "Virtual Classroom", "VR learning experience"),
            Create(Topic17, Track17, "Charity Platform", "Donation platform"),
            Create(Topic18, Track18, "Any Idea", "Open topic"),
            Create(Topic19, Track19, "AI Model", "AI model development"),
            Create(Topic20, Track20, "Legacy Web App", "Web application"),
            Create(Topic21, Track21, "Mobile Game", "Mobile game"),
            Create(Topic22, Track22, "Summer AI App", "AI application"),
            Create(Topic23, Track23, "Summer Web App", "Web application"),
            Create(Topic24, Track24, "Winter ML", "ML project"),
            Create(Topic25, Track25, "Winter Portal", "Web portal"),
            Create(Topic26, Track26, "Old Topic", "Disabled topic"),
            Create(Topic27, Track27, "Old Topic E4", "Disabled topic"),
            Create(Topic28, Track28, "Old Topic E7", "Disabled topic"),
            Create(Topic29, Track29, "Old Topic E10", "Disabled topic"),
            Create(Topic30, Track30, "Old Topic E3", "Disabled topic")
        );
    }

    private static Tracks Create(Guid id, Guid eventId, string title, string desc, int maxTeam, bool isDisable = false) => new()
    {
        Id = id, EventId = eventId, Title = title, Description = desc, MaxTeam = maxTeam,
        IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static Topics Create(Guid id, Guid trackId, string title, string desc) => new()
    {
        Id = id, TrackId = trackId, Title = title, Description = desc,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
