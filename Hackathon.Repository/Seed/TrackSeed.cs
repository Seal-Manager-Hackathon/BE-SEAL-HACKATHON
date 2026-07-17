using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TrackSeed
{
    // Tracks
    public static readonly Guid Track1Ai = Guid.Parse("24000000-0000-0000-0000-000000000001");
    public static readonly Guid Track2Web = Guid.Parse("24000000-0000-0000-0000-000000000002");
    public static readonly Guid Track3Mobile = Guid.Parse("24000000-0000-0000-0000-000000000003");
    public static readonly Guid Track4Iot = Guid.Parse("24000000-0000-0000-0000-000000000004");
    public static readonly Guid Track5Cloud = Guid.Parse("24000000-0000-0000-0000-000000000005");
    public static readonly Guid Track6Security = Guid.Parse("24000000-0000-0000-0000-000000000006");
    public static readonly Guid Track7Blockchain = Guid.Parse("24000000-0000-0000-0000-000000000007");
    public static readonly Guid Track8Game = Guid.Parse("24000000-0000-0000-0000-000000000008");
    public static readonly Guid Track9Data = Guid.Parse("24000000-0000-0000-0000-000000000009");
    public static readonly Guid Track10Devops = Guid.Parse("24000000-0000-0000-0000-000000000010");

    // Topics
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

    public static void SeedTracks(this ModelBuilder modelBuilder)
    {
        // 10 Tracks (distributed across Event 2 and Event 4 published events for testing)
        modelBuilder.Entity<Tracks>().HasData(
            new Tracks { Id = Track1Ai, EventId = SeedConstants.Event2Published, Title = "Trí tuệ nhân tạo (AI)", Description = "Phát triển các mô hình AI", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track2Web, EventId = SeedConstants.Event2Published, Title = "Ứng dụng Web", Description = "Xây dựng web app", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track3Mobile, EventId = SeedConstants.Event2Published, Title = "Ứng dụng Di động", Description = "Phát triển app mobile", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track4Iot, EventId = SeedConstants.Event2Published, Title = "Internet vạn vật (IoT)", Description = "Hệ thống phần cứng nhúng", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track5Cloud, EventId = SeedConstants.Event2Published, Title = "Điện toán đám mây", Description = "Hạ tầng đám mây", MaxTeam = 5, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track6Security, EventId = SeedConstants.Event4Published, Title = "Bảo mật hệ thống", Description = "An toàn thông tin", MaxTeam = 5, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track7Blockchain, EventId = SeedConstants.Event4Published, Title = "Công nghệ Blockchain", Description = "Hợp đồng thông minh", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track8Game, EventId = SeedConstants.Event4Published, Title = "Phát triển Game", Description = "Thiết kế game", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track9Data, EventId = SeedConstants.Event4Published, Title = "Khoa học Dữ liệu", Description = "Phân tích dữ liệu lớn", MaxTeam = 10, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Tracks { Id = Track10Devops, EventId = SeedConstants.Event4Published, Title = "DevOps & CI/CD", Description = "Tự động hóa", MaxTeam = 5, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 10 Topics (linked to their respective tracks)
        modelBuilder.Entity<Topics>().HasData(
            new Topics { Id = Topic1, TrackId = Track1Ai, Title = "Ứng dụng LLM trong Giáo dục", Description = "Xây dựng Chatbot AI", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic2, TrackId = Track2Web, Title = "Nền tảng thương mại điện tử", Description = "Hệ thống quản lý sản phẩm", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic3, TrackId = Track3Mobile, Title = "App theo dõi sức khỏe", Description = "Theo dõi bước chân và calo", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic4, TrackId = Track4Iot, Title = "Nhà thông minh (Smart Home)", Description = "Điều khiển thiết bị qua Wi-Fi", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic5, TrackId = Track5Cloud, Title = "Serverless REST API", Description = "Triển khai trên AWS Lambda", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic6, TrackId = Track6Security, Title = "Hệ thống phát hiện xâm nhập", Description = "IDS phân tích log", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic7, TrackId = Track7Blockchain, Title = "NFT Marketplace", Description = "Giao dịch tài sản số", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic8, TrackId = Track8Game, Title = "Game 2D đi ải", Description = "Phát triển bằng Unity", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic9, TrackId = Track9Data, Title = "Phân tích hành vi khách hàng", Description = "Gợi ý sản phẩm", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Topics { Id = Topic10, TrackId = Track10Devops, Title = "GitLab CI/CD Pipeline", Description = "Tự động build và deploy", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
