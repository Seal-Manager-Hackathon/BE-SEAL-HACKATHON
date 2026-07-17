using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TrackSeed
{
    // 20 Tracks (10 per main published event)
    public static readonly Guid Track1Ai  = Guid.Parse("24000000-0000-0000-0000-000000000001");
    public static readonly Guid Track2Web = Guid.Parse("24000000-0000-0000-0000-000000000002");
    public static readonly Guid Track3Mobile = Guid.Parse("24000000-0000-0000-0000-000000000003");
    public static readonly Guid Track4Iot = Guid.Parse("24000000-0000-0000-0000-000000000004");
    public static readonly Guid Track5Cloud = Guid.Parse("24000000-0000-0000-0000-000000000005");
    public static readonly Guid Track6Security = Guid.Parse("24000000-0000-0000-0000-000000000006");
    public static readonly Guid Track7Blockchain = Guid.Parse("24000000-0000-0000-0000-000000000007");
    public static readonly Guid Track8Game = Guid.Parse("24000000-0000-0000-0000-000000000008");
    public static readonly Guid Track9Data = Guid.Parse("24000000-0000-0000-0000-000000000009");
    public static readonly Guid Track10Devops = Guid.Parse("24000000-0000-0000-0000-000000000010");

    public static readonly Guid Track11Ai  = Guid.Parse("24000000-0000-0000-0000-000000000011");
    public static readonly Guid Track12Web = Guid.Parse("24000000-0000-0000-0000-000000000012");
    public static readonly Guid Track13Mobile = Guid.Parse("24000000-0000-0000-0000-000000000013");
    public static readonly Guid Track14Iot = Guid.Parse("24000000-0000-0000-0000-000000000014");
    public static readonly Guid Track15Cloud = Guid.Parse("24000000-0000-0000-0000-000000000015");
    public static readonly Guid Track16Security = Guid.Parse("24000000-0000-0000-0000-000000000016");
    public static readonly Guid Track17Blockchain = Guid.Parse("24000000-0000-0000-0000-000000000017");
    public static readonly Guid Track18Game = Guid.Parse("24000000-0000-0000-0000-000000000018");
    public static readonly Guid Track19Data = Guid.Parse("24000000-0000-0000-0000-000000000019");
    public static readonly Guid Track20Devops = Guid.Parse("24000000-0000-0000-0000-000000000020");

    // 20 Topics (2 per track)
    public static readonly Guid Topic1  = Guid.Parse("25000000-0000-0000-0000-000000000001");
    public static readonly Guid Topic2  = Guid.Parse("25000000-0000-0000-0000-000000000002");
    public static readonly Guid Topic3  = Guid.Parse("25000000-0000-0000-0000-000000000003");
    public static readonly Guid Topic4  = Guid.Parse("25000000-0000-0000-0000-000000000004");
    public static readonly Guid Topic5  = Guid.Parse("25000000-0000-0000-0000-000000000005");
    public static readonly Guid Topic6  = Guid.Parse("25000000-0000-0000-0000-000000000006");
    public static readonly Guid Topic7  = Guid.Parse("25000000-0000-0000-0000-000000000007");
    public static readonly Guid Topic8  = Guid.Parse("25000000-0000-0000-0000-000000000008");
    public static readonly Guid Topic9  = Guid.Parse("25000000-0000-0000-0000-000000000009");
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

    public static void SeedTracks(this ModelBuilder modelBuilder)
    {
        // Event 2 tracks
        modelBuilder.Entity<Tracks>().HasData(
            Create(Track1Ai,  SeedConstants.Event2Published, "Trí tuệ nhân tạo (AI)",   "Phát triển mô hình AI",       10),
            Create(Track2Web, SeedConstants.Event2Published, "Ứng dụng Web",            "Xây dựng web app",            10),
            Create(Track3Mobile, SeedConstants.Event2Published, "Ứng dụng Di động",     "Phát triển app mobile",       10),
            Create(Track4Iot, SeedConstants.Event2Published, "Internet vạn vật (IoT)",  "Hệ thống phần cứng nhúng",    10),
            Create(Track5Cloud, SeedConstants.Event2Published, "Điện toán đám mây",      "Hạ tầng đám mây",             5)
        );
        // Event 4 tracks
        modelBuilder.Entity<Tracks>().HasData(
            Create(Track6Security,  SeedConstants.Event4Published, "Bảo mật hệ thống",   "An toàn thông tin",           5),
            Create(Track7Blockchain,SeedConstants.Event4Published, "Công nghệ Blockchain","Hợp đồng thông minh",        10),
            Create(Track8Game,     SeedConstants.Event4Published,   "Phát triển Game",     "Thiết kế game",              10),
            Create(Track9Data,     SeedConstants.Event4Published,   "Khoa học Dữ liệu",    "Phân tích dữ liệu lớn",      10),
            Create(Track10Devops,  SeedConstants.Event4Published,   "DevOps & CI/CD",      "Tự động hóa",                 5)
        );
        // Event 7 tracks
        modelBuilder.Entity<Tracks>().HasData(
            Create(Track11Ai,  SeedConstants.Event7Published, "AI - Xử lý ảnh",          "Xử lý ảnh thông minh",        8),
            Create(Track12Web, SeedConstants.Event7Published, "Web - Thương mại điện tử", "Nền tảng TMĐT",               8),
            Create(Track13Mobile, SeedConstants.Event7Published, "Mobile - Game",         "Phát triển game mobile",      8),
            Create(Track14Iot, SeedConstants.Event7Published, "IoT - Nhà thông minh",     "Smart Home",                  8),
            Create(Track15Cloud, SeedConstants.Event7Published, "Cloud - DevOps",         "Hạ tầng tự động",             5)
        );
        // Event 10 tracks
        modelBuilder.Entity<Tracks>().HasData(
            Create(Track16Security,  SeedConstants.Event10Published, "An ninh mạng",      "Phòng chống tấn công",        8),
            Create(Track17Blockchain,SeedConstants.Event10Published, "Blockchain - NFT",  "Tài sản số",                  8),
            Create(Track18Game,     SeedConstants.Event10Published,   "Game - Metaverse",  "Thực tế ảo",                  8),
            Create(Track19Data,     SeedConstants.Event10Published,   "Data - Big Data",   "Xử lý dữ liệu lớn",           8),
            Create(Track20Devops,  SeedConstants.Event10Published,    "DevOps - Kubernetes","Container orchestration",     5)
        );

        // Topics
        modelBuilder.Entity<Topics>().HasData(
            // Event 2 tracks - 1 topic each
            Create(Topic1,  Track1Ai,  "LLM trong Giáo dục",     "Chatbot AI hỗ trợ học tập"),
            Create(Topic2,  Track2Web, "Nền tảng thương mại",    "Hệ thống quản lý sản phẩm"),
            Create(Topic3,  Track3Mobile, "App theo dõi sức khỏe","Theo dõi bước chân và calo"),
            Create(Topic4,  Track4Iot, "Nhà thông minh",         "Điều khiển thiết bị qua Wi-Fi"),
            Create(Topic5,  Track5Cloud, "Serverless REST API",  "Triển khai AWS Lambda"),
            // Event 4 tracks - 1 topic each
            Create(Topic6,  Track6Security, "Hệ thống phát hiện xâm nhập", "IDS phân tích log"),
            Create(Topic7,  Track7Blockchain, "NFT Marketplace",  "Giao dịch tài sản số"),
            Create(Topic8,  Track8Game, "Game 2D đi ải",         "Phát triển bằng Unity"),
            Create(Topic9,  Track9Data, "Phân tích hành vi khách hàng", "Gợi ý sản phẩm"),
            Create(Topic10, Track10Devops, "GitLab CI/CD Pipeline","Tự động build và deploy"),
            // Event 7 tracks - 1 topic each
            Create(Topic11, Track11Ai,  "Nhận diện khuôn mặt",   "AI emotion recognition"),
            Create(Topic12, Track12Web, "Nền tảng đấu giá",      "Auction platform"),
            Create(Topic13, Track13Mobile, "Game giáo dục",      "Học qua trò chơi"),
            Create(Topic14, Track14Iot, "Hệ thống tưới tiêu tự động","Smart farming"),
            Create(Topic15, Track15Cloud, "Monitoring Platform",  "Hạ tầng giám sát"),
            // Event 10 tracks - 1 topic each
            Create(Topic16, Track16Security, "Phần mềm diệt malware","Phát hiện mã độc"),
            Create(Topic17, Track17Blockchain, "Ví điện tử",      "Crypto wallet"),
            Create(Topic18, Track18Game, "Game thực tế ảo",      "VR experience"),
            Create(Topic19, Track19Data, "Recommendation System", "Hệ thống gợi ý"),
            Create(Topic20, Track20Devops, "Kubernetes Cluster",  "Auto-scaling cluster")
        );
    }

    private static Tracks Create(Guid id, Guid eventId, string title, string desc, int maxTeam)
        => new() { Id = id, EventId = eventId, Title = title, Description = desc, MaxTeam = maxTeam, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static Topics Create(Guid id, Guid trackId, string title, string desc)
        => new() { Id = id, TrackId = trackId, Title = title, Description = desc, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
