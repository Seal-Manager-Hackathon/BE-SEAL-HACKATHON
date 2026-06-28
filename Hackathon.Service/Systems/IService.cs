using Microsoft.AspNetCore.Http;

namespace Hackathon.Service.Systems;

public interface IService
{
    Dictionary<string, Dictionary<string, string>> GetEnums();
    Task<Response.HealthResponse> GetHealth(DateTime startupTime);
    Response.VersionResponse GetVersion(string environmentName);
    Task<Response.UploadFileResponse> UploadFile(IFormFile? file, string? folder);
}
