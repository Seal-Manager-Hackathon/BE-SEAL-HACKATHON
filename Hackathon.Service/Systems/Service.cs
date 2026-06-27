using Hackathon.Repository;
using Hackathon.Repository.Enum;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Service.Systems;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly CloudinaryOptions _cloudinaryOptions = new();

    public Service(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        configuration.GetSection("CloudinaryOptions").Bind(_cloudinaryOptions);
    }

    public Dictionary<string, Dictionary<string, string>> GetEnums()
    {
        var enumsToExtract = new List<Type>
        {
            typeof(EmailVerificationStatusEnum),
            typeof(EventRoleEnum),
            typeof(EventStatusEnum),
            typeof(InvitationStatusEnum),
            typeof(LeaderBoardsStatusEnum),
            typeof(NotificationStatusEnum),
            typeof(RegisterTeamStatusEnum),
            typeof(ReportStatusEnum),
            typeof(RoleEnum),
            typeof(ScoresStatusEnum),
            typeof(SubmissionStatusEnum),
            typeof(TeamDetailStatusEnum),
            typeof(UserStatusEnum)
        };

        var data = new Dictionary<string, Dictionary<string, string>>();
        foreach (var type in enumsToExtract)
        {
            var enumDict = new Dictionary<string, string>();
            foreach (var value in Enum.GetValues(type))
            {
                var intValue = (int)value;
                var name = value.ToString() ?? string.Empty;
                enumDict[intValue.ToString()] = name;
            }
            data[type.Name] = enumDict;
        }

        return data;
    }

    public async Task<Response.HealthResponse> GetHealth(DateTime startupTime)
    {
        bool databaseConnected = await _dbContext.Database.CanConnectAsync();

        if (!databaseConnected)
        {
            throw new ServiceUnavailableException("DATABASE_CONNECTION_LOST", "DATABASE_CONNECTION_LOST");
        }

        return new Response.HealthResponse
        {
            Status = 1,
            Database = 1,
            UptimeSeconds = (int)(DateTime.UtcNow - startupTime).TotalSeconds
        };
    }

    public Response.VersionResponse GetVersion(string environmentName)
    {
        var assemblyVersion = typeof(Service).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "1.0.0";

        if (assemblyVersion.Contains('+'))
        {
            assemblyVersion = assemblyVersion.Split('+')[0];
        }

        return new Response.VersionResponse
        {
            Version = $"{assemblyVersion}-build.20260622",
            Environment = environmentName,
            DotnetVersion = ".NET 8.0"
        };
    }

    public async Task<Response.UploadFileResponse> UploadFile(IFormFile? file, string? folder)
    {
        if (file == null || file.Length == 0)
        {
            throw new BadRequestException("FILE_REQUIRED", "FILE_REQUIRED");
        }

        const long maxFileSize = 10 * 1024 * 1024; // 10MB
        if (file.Length > maxFileSize)
        {
            throw new BadRequestException("FILE_SIZE_LIMIT_EXCEEDED", "FILE_SIZE_LIMIT_EXCEEDED");
        }

        var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".zip" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadRequestException("INVALID_FILE_TYPE", "INVALID_FILE_TYPE");
        }

        var originalFileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
        var cleanFileName = originalFileNameWithoutExt.Replace(" ", "_");
        var uniqueFileName = $"{cleanFileName}_{Guid.NewGuid()}{extension}";

        try
        {
            var account = new Account(
                _cloudinaryOptions.CloudName,
                _cloudinaryOptions.ApiKey,
                _cloudinaryOptions.ApiSecret
            );
            var cloudinary = new Cloudinary(account);

            using var stream = file.OpenReadStream();

            RawUploadParams uploadParams;
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
            {
                uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(uniqueFileName, stream),
                    Folder = folder ?? "uploads"
                };
            }
            else
            {
                uploadParams = new RawUploadParams
                {
                    File = new FileDescription(uniqueFileName, stream),
                    Folder = folder ?? "uploads"
                };
            }

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null || uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMsg = uploadResult.Error?.Message ?? "Cloudinary upload failed";
                throw new FileUploadFailedException(errorMsg);
            }

            return new Response.UploadFileResponse
            {
                FileUrl = uploadResult.SecureUrl.ToString(),
                FileName = uniqueFileName,
                FileSize = file.Length
            };
        }
        catch (AppException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new FileUploadFailedException(ex.Message);
        }
    }
}
